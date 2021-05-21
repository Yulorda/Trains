using System.Collections;
using UnityEngine;

    public class FollowMouseMover : MonoBehaviour
    {
        private TrainStation connector;

        public void Inject(TrainStation connector)
        {
            this.connector = connector;
        }

        public void StartFollow()
        {
            StartCoroutine(StartFollowing());
        }

        private IEnumerator StartFollowing()
        {
            while (true)
            {
                yield return null;
                Following();

                if (Input.GetMouseButtonUp(0))
                {
                    EndFollow();
                    break;
                }
            }
        }

        private void Following()
        {
            connector.Position.SetValueAndForceNotify(GetPosition());
        }

        public void EndFollow()
        {
            connector.OnMouseUp.Execute();
        }

        private Vector3 GetPosition()
        {
            var position = connector.Position.Value;
            Vector3 distanceToScreen = Camera.main.WorldToScreenPoint(position);
            Vector3 positionResult = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen.z));
            return new Vector3(positionResult.x, position.y, positionResult.z);
        }
    }
