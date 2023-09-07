using UnityEngine;
using System.Collections;

///Developed By Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class Line : MonoBehaviour
{
		/// <summary>
		/// The color of the line.
		/// </summary>
		public Color lineColor;

		/// <summary>
		/// The line material.
		/// </summary>
		public Material lineMaterial;

		/// <summary>
		/// The line texture.
		/// </summary>
    	public Texture lineTexture;

		/// <summary>
		/// The first,second points.
		/// </summary>
		[HideInInspector]
		public Vector3 firstPoint, secondPoint;

		/// <summary>
		/// The line renderer component.
		/// </summary>
		private LineRenderer lineRenderer;

		/// <summary>
		/// The width of the line.
		/// </summary>
		[Range(0,1)]
		public float lineWidth = 0.22f;

		/// <summary>
		/// The line sorting order.
		/// </summary>
		[Range(-50,50)]
		public int lineSortingOrder;

		/// <summary>
		/// The first point Z position.
		/// </summary>
		[Range(-20,20)]
		public float firstPointZPosition = 1;

		/// <summary>
		/// The second point Z position.
		/// </summary>
		[Range(-20,20)]
		public float secondPointZPosition = 1;

		/// <summary>
		/// The lerp speed.
		/// </summary>
		[Range(0,10)]
		public float lerpSpeed = 5;

		/// <summary>
		/// Whether the line is lerping.
		/// </summary>
		public bool isLerping;

		/// <summary>
		/// The line arrow.
		/// </summary>
		public Transform lineArrow;

		/// <summary>
		/// The line arrow euler angles.
		/// </summary>
		private Vector3 lineArrowEulerAngles;

		/// <summary>
		/// The direction between click position and the selected dot.
		/// </summary>
		private Vector3 direction;

		void Start ()
		{
				InitLine ();
		}

		void Update ()
		{
				#if UNITY_EDITOR
					if(!Application.isPlaying){
						InitLine();
					}
				#endif

				if (lineArrow != null) {
					direction = firstPoint - lineArrow.transform.position;
					lineArrowEulerAngles = lineArrow.eulerAngles;
					lineArrowEulerAngles.z = Mathf.Atan2 (direction.x, -direction.y) * Mathf.Rad2Deg;
					lineArrow.eulerAngles = lineArrowEulerAngles;
				}

				if (isLerping) {
						LerpToFirstPoint ();
				}
		}

		/// <summary>
		/// Init the line.
		/// </summary>
		public void InitLine ()
		{
				if (lineRenderer == null) {
						lineRenderer = GetComponent<LineRenderer> ();
						lineRenderer.SetVertexCount (2);
				}
		
				if (lineMaterial == null) {
						lineMaterial = new Material (Shader.Find ("GUI/Text Shader"));//Default material
						if(lineTexture!=null)
							lineMaterial.mainTexture = lineTexture;
				}

				lineRenderer.sortingOrder = this.lineSortingOrder;
				lineRenderer.material = lineMaterial;
				lineRenderer.SetWidth (lineWidth, lineWidth);
				SetColor (lineColor);
		}

		/// <summary>
		/// Set the first point.
		/// </summary>
		/// <param name="point">Point.</param>
		public void SetFirstPoint (Vector3 point)
		{
				point.z = firstPointZPosition;
				this.firstPoint = point;
				lineRenderer.SetPosition (0, point);
		}

		/// <summary>
		/// Set the second point.
		/// </summary>
		/// <param name="point">Point.</param>
		public void SetSecondPoint (Vector3 point)
		{
				point.z = secondPointZPosition;
				this.secondPoint = point;
				lineRenderer.SetPosition (1, point);
		}

		/// <summary>
		/// Refresh the first point.
		/// </summary>
		public void RefreshFirstPoint ()
		{
				lineRenderer.SetPosition (1, firstPoint);
		}

		/// <summary>
		/// Refresh the second point.
		/// </summary>
		public void RefreshSecondPoint ()
		{
				lineRenderer.SetPosition (1, secondPoint);
		}
		
		/// <summary>
		/// Reset the line.
		/// </summary>
		public void Reset ()
		{
				SetFirstPoint (Vector3.zero);
				SetSecondPoint (Vector3.zero);
				isLerping = false;
				if (lineArrow != null) {
					lineArrow.transform.position = Vector3.zero;
					lineArrow.GetComponent<SpriteRenderer> ().enabled = false;
				}
		}

		/// <summary>
		/// Set the color.
		/// </summary>
	 	/// <param name="color">Color.</param>
		public void SetColor(Color color){
			if (lineRenderer != null) {
				lineMaterial.color = color;
				lineRenderer.SetColors (color, color);
				lineColor = color;
			if(lineArrow!=null)
				lineArrow.GetComponent<SpriteRenderer> ().color = color;
			}
		}

		/// <summary>
		/// Set the sorting order.
		/// </summary>
		public void SetSortingOrder(int lineSortingOrder ){
			this.lineSortingOrder = lineSortingOrder;
			if (lineRenderer != null) {
				lineRenderer.sortingOrder = lineSortingOrder;
			}
		}

		/// <summary>
		/// Lerp to the first point.
		/// </summary>
		public void LerpToFirstPoint ()
		{
				secondPoint.x = Mathf.Lerp (secondPoint.x, firstPoint.x, lerpSpeed * Time.deltaTime);
				secondPoint.y = Mathf.Lerp (secondPoint.y, firstPoint.y, lerpSpeed * Time.deltaTime);
				if (lineArrow != null) {
					lineArrow.transform.position = secondPoint;
					if (Vector2.Distance (lineArrow.position, firstPoint) <= 0.1f) {
						lineArrow.GetComponent<SpriteRenderer> ().enabled = false;
						secondPoint = firstPoint;
						isLerping = false;
					}
				}
				RefreshSecondPoint ();
		}
}