using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CryptoGame {

    [System.Serializable]
    public class CharacterMovement : CharacterComponent {

        [SerializeField][ReadOnly] NavMeshAgent navMeshAgent;
        [SerializeField][ReadOnly] Vector3 destination;
        [ReadOnly] CharacterAnimator characterAnimator;

        [Header("PathFinder")]
        [SerializeField] PathBehavior pathBehavior;
        [SerializeField][ReadOnly] Node closestNode;
        [SerializeField][ReadOnly] Node destinationNode;
        [SerializeField][ReadOnly] List<Node> pathFound;
        [SerializeField][ReadOnly] float radius = 7f;

        float animationSpeed;

        public void SetSpeed(float newSpeed)
        {
            this.navMeshAgent.speed = newSpeed;
        }

        public void SetAngularSpeed(float newAngularSpeed)
        {
            this.navMeshAgent.angularSpeed = newAngularSpeed;
        }

        public void SetAcceleration(float newAcceleration)
        {
            this.navMeshAgent.acceleration = newAcceleration;
        }

        public CharacterMovement (CharacterCommander commander, NavMeshAgent navMeshAgent) : base( commander )
        {
            this.navMeshAgent = navMeshAgent;
            SetSpeed( this.Commander.CharacterParameters.Movement.WalkSpeed );
            SetAngularSpeed( this.Commander.CharacterParameters.Movement.AngularSpeed );
            SetAcceleration( this.Commander.CharacterParameters.Movement.Acceleration );
            characterAnimator = this.Commander.CharacterAnimator;
            this.Disable();
        }

        public void Move(Vector3 destination)
        {
            this.destination = destination;
            this.navMeshAgent.SetDestination( this.destination );
        }

        public override void Update (){
            UpdateAnimations();
        }
        

        private void UpdateAnimations()
        {
            float distanceToDestination = Vector3.Distance( this.Commander.transform.position, destination );

            //if (distanceToDestination >= 5)
            //{
            //    characterAnimator.SetSpeed( 2f );
            //    this.navMeshAgent.speed = this.Commander.CharacterParameters.Movement.RunSpeed;
            //    Debug.Log( "long distance" );
            //}
            //else
            if (distanceToDestination >= 1.12f 
                //& distanceToDestination < 5
                )
            {
                characterAnimator.SetSpeed( 1f );
                this.navMeshAgent.speed = ( this.Commander.CharacterParameters.Movement.WalkSpeed );
            }
            else
            {
                characterAnimator.SetSpeed( 0f );
                this.navMeshAgent.speed = this.Commander.CharacterParameters.Movement.WalkSpeed;
            }

            if (this.navMeshAgent.velocity.magnitude == 0)
            {
                characterAnimator.SetSpeed( 0f );
                this.navMeshAgent.speed = this.Commander.CharacterParameters.Movement.WalkSpeed;
            }

            //this.navMeshAgent.velocity
        }

        public void SetDestination(Node destination)
        {
            this.destinationNode = destination;
        }

        private Node FindAnyNode ()
        {
            Node nodeFound = null;

            Collider [ ] collidersFound = Physics.OverlapSphere( this.Commander.transform.position, radius );
            foreach (Collider collider in collidersFound)
            {
                nodeFound = collider.GetComponent<Node>();
                if (nodeFound != null)
                    break;
            }

            closestNode = nodeFound;
            return nodeFound;
        }

        private bool ArrivedAtNode (Node node)
        {
            float distance = Vector3.Distance( node.transform.position, this.Commander.transform.position );
            if (distance <= 1f)
                return true;

            return false;
        }

        public void FollowPath (PathBehavior pathBehavior = PathBehavior.OneWay)
        {
            this.pathBehavior = pathBehavior;
            FindPathToDestinationNode();
            this.Commander.StartCoroutine( FollowPathCo() );
        }

        public void FindPathToDestinationNode ()
        {
            Node start = FindAnyNode();

            if (start == null)
            {
                Debug.LogError( "Could Not Find Start Node" );
                return;
            }

            if (destinationNode == null)
            {
                Debug.LogError( "CharacterMovement: destinationNode not set. Cannot Follow Path Co", this.Commander.gameObject );
                return;
            }
                
            pathFound = Pathfinder.FindShortestPath( start, destinationNode );
        }

        IEnumerator FollowPathCo ()
        {
            Node node;
            Node nexNode;

            bool pingPongGoBack = false;

            for (int i = 0; i < pathFound.Count; i++)
            {
                node = pathFound [ i ];
                float offset = Random.Range( 0f, .25f );
                Vector3 randomCloseDestination = new Vector3( node.transform.position.x + offset, node.transform.position.y, node.transform.position.z + offset );
                //navMeshAgent.SetDestination( node.transform.position );
                navMeshAgent.SetDestination( randomCloseDestination );
                while (!ArrivedAtNode( node ))
                    yield return null;

                if (i != pathFound.Count - 1)
                {
                    nexNode = pathFound [ i + 1 ];
                    if (node.NodeStatus == NodeStatus.Wait && nexNode.NodeStatus == NodeStatus.Wait)
                    {
                        navMeshAgent.isStopped = true;
                        while (node.NodeStatus == NodeStatus.Wait)
                            yield return null;
                        navMeshAgent.isStopped = false;
                    }
                }

                if (pathBehavior == PathBehavior.PingPong)
                {
                    if (i == 0)
                        pingPongGoBack = false;
                    else if (i == pathFound.Count - 1)
                        pingPongGoBack = true;

                    if (pingPongGoBack)
                        i -= 2;
                }

                else if (pathBehavior == PathBehavior.RestartAtEnd)
                {
                    if (i == pathFound.Count - 1)
                        i = -1;
                }

                else if (pathBehavior == PathBehavior.Explorer)
                {

                    if (i == pathFound.Count - 1)
                    {
                        FindAnyNode();
                        this.Commander.FindRandomDestination();
                        FindPathToDestinationNode();
                        i = 0;
                    }
                        
                }
            }
            yield return null;
        }
    }

}