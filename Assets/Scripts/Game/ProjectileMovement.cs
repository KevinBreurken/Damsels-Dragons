using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Base.Game {

    
    public class ProjectileMovement : MonoBehaviour {

        public LayerMask hitMask;
        public Ease easeType;
        
        public void StartMove () {

            Vector2 moveToPosition = GetMoveToPosition();
            transform.DOMoveX(moveToPosition.x,1);
            transform.DOMoveY(moveToPosition.y + 1.5f, 1).OnComplete(Bounce);

        }

        public void Bounce () {

            Vector2 moveToPosition = GetMoveToPosition();
            moveToPosition.y += 1.5f;
            transform.DOJump(moveToPosition, 5, 1, 1).OnComplete(Bounce).SetEase(easeType);
            
        }

        void Update () {

            if (Input.GetKeyDown(KeyCode.G)) {

                Bounce();

            }

        }

        private Vector2 GetMoveToPosition () {

            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + new Vector2(-3, 10), -Vector2.up, 20, hitMask.value);
            if (hit.collider != null) {

              
                return hit.transform.position;
            }

            return new Vector2(0, 0);

        }

    }

}