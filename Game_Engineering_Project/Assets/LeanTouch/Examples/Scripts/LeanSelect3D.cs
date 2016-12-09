using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to select a GameObject using any finger, as long it has a collider
	public class LeanSelect3D : MonoBehaviour
	{
		[Tooltip("This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)")]
		public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;
		
		[Tooltip("The currently selected GameObject")]
		public GameObject SelectedGameObject;

		[Tooltip("Change the color of the selected GameObject?")]
		public bool ColorSelected = true;

		[Tooltip("The color of the selected GameObject")]
		public Color SelectedColor = Color.green;


        public MainGameControls gameController;



        protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerTap += OnFingerTap;
		}
		
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerTap -= OnFingerTap;
		}
		
		public void OnFingerTap(LeanFinger finger)
		{
			// Raycast information
			var ray = finger.GetRay();
			var hit = default(RaycastHit);
			
			// Was this finger pressed down on a collider?
			if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask))
			{
                if (hit.collider.gameObject == GameObject.FindGameObjectWithTag("PlayerOne") && gameController.getCurrentTurn())
                {
                    Select(hit.collider.gameObject);
                    gameController.showMovementDirections(0);
                }
                else if(hit.collider.gameObject == GameObject.FindGameObjectWithTag("PlayerTwo") && !gameController.getCurrentTurn())
                {
                    Select(hit.collider.gameObject);
                    gameController.showMovementDirections(1);
                }
                if(hit.collider.gameObject.tag == "topDirection" || hit.collider.gameObject.tag == "rightDirection" || hit.collider.gameObject.tag == "bottomDirection" || hit.collider.gameObject.tag == "leftDirection")
                {
                    checkIfDirectionFieldWasClicked(hit.collider.gameObject.tag);
                }
			}

            /*
			else
			{
				// Nothing was tapped, so deselect
				Deselect();
			}
            */
		}


        private void checkIfDirectionFieldWasClicked(string direction)
        {
            int selectedDirection = 0;

            if(direction == "topDirection")
            {
                selectedDirection = 0;
            }
            else if(direction == "rightDirection")
            {
                selectedDirection = 1;
            }
            else if(direction == "bottomDirection")
            {
                selectedDirection = 2;
            }
            else if(direction == "leftDirection")
            {
                selectedDirection = 3;
            }
            gameController.removeMoveIndikator();
            gameController.moveSelectedCharacter(selectedDirection);
        }




        private void Deselect()
		{
			// Is there a selected GameObject?
			if (SelectedGameObject != null)
			{
				// Mark selected GameObject null
				SelectedGameObject = null;
			}
		}

		private void Select(GameObject newGameObject)
		{
			// Has the selected GameObject changed?
			if (newGameObject != SelectedGameObject)
			{
				// Deselect the old GameObject
				Deselect();

				// Change selection
				SelectedGameObject = newGameObject;

			}
            else if(newGameObject == SelectedGameObject)
            {
                Deselect();
            }
		}
	}
}