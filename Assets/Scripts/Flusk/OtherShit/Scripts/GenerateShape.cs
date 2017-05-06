using UnityEngine;
using System.Collections;
using Flusk.MeshGeneration;


namespace Flusk.MeshGeneration.Shapes
{
    public enum ShapeTypes
    {
        Circle = 0,
        Triangle = 1,
        Quad = 2,
        Pentagon = 3,
        Hexagon = 4
    }

    [ExecuteInEditMode]
	public class GenerateShape : MonoBehaviour {

        [SerializeField]
        private ShapeTypes shape;

        [SerializeField]
		[InspectorButton("Triangle")] bool triangle;
		
		[SerializeField]
		[InspectorButton("Quad")] bool quad;
		
		[SerializeField]
		[InspectorButton("Pent")] bool pent;

		[SerializeField]
		[InspectorButton("Shex")] bool hex;
		
		public float length = 1;

        [SerializeField]
        private bool change = true;

        private void OnEnable()
        {
            Make();
        }

        private void OnLevelWasLoaded (int num)
        {
            Make();
        }

        private void Make ()
        {
            if (change)
            {
                switch (shape)
                {
                    case ShapeTypes.Circle:
                    {
                        return;
                    }

                    case ShapeTypes.Triangle:
                    {
                        Triangle();
                        break;
                    }

                    case ShapeTypes.Quad:
                    {
                        Quad();
                        break;
                    }

                    case ShapeTypes.Pentagon:
                    {
                        Pent();
                        break;
                    }

                    case ShapeTypes.Hexagon:
                    {
                        Shex();
                        break;
                    }
                }
            }
        }
        #region BuildFunctions
        private void Triangle ()
		{
			MeshFilter filter = GetComponent<MeshFilter>();
			MeshBuilder meshBuilder = new MeshBuilder();
			
			float half = length * 0.5f;
			float baseLength = length / Mathf.Sin( 60 * Mathf.Deg2Rad );
			

            float thirtyDegres = 30 * Mathf.Deg2Rad;
            float sixty = thirtyDegres * 2;
            float ninety = thirtyDegres * 3;

            float halfHeight = Mathf.Sin(thirtyDegres) / Mathf.Sin(ninety) * length;
            baseLength = Mathf.Sin(sixty) / Mathf.Sin(ninety) * length;
            float halfBase = baseLength * 0.5f;

            Vector3 downAngle = angleToVector(ninety + ninety + sixty);
            downAngle *= halfHeight;

			//point 0
			Vector3 vert1 = new Vector3( 0, halfHeight);
			Vector3 vert2 = new Vector3( -halfBase, downAngle.y );
            Vector3 vert3 = new Vector3( halfBase, downAngle.y );
			
			meshBuilder.AddVerts( vert1, vert2, vert3 );
			meshBuilder.AddUVS( Vector3.back, Vector3.back, Vector3.back);
			meshBuilder.AddTriangle( 0, 1, 2);
			
			Mesh mesh = meshBuilder.CreateMesh();
			filter.sharedMesh = mesh;
		}
		
		private void Quad ()
		{
			MeshFilter filter = GetComponent<MeshFilter>();
			MeshBuilder b = new MeshBuilder();
			BuildQuad( b, Vector3.zero );
			Mesh m = b.CreateMesh();
			filter.sharedMesh = m;
		}

		private void SlowPentagon()
		{
			//StartCoroutine( SlowBuild() );
			//start = true;
		}

		[SerializeField]
		private bool start = false;
		private void Update ()
		{
			if ( start )
			{
				//StartCoroutine( SlowBuild() );
			}
		}

		[SerializeField]
		Vector3 [] p = new Vector3 [6];
		MeshBuilder meshBuilder;
		Mesh mesh;
		MeshFilter filter;
		[SerializeField]
		int currentI = 0;

		private float currentAngle = 0;
		float angle = ( 2 * Mathf.PI ) / 5;
		[SerializeField]
		private GameObject point;

		private void Pent ()
		{
			currentAngle = Mathf.PI / 2f;
			meshBuilder = new MeshBuilder();
			filter = GetComponent<MeshFilter>();
			filter.sharedMesh = null;
			mesh = new Mesh();
			Vector3 currentPoint = Vector3.zero;
			for ( int i = 0; i <= 6; i++ )
			{
				if ( i == 6 )
				{
					meshBuilder.AddTriangle( 0, i - 1, 1 );
				}
				else if ( i > 1 )
				{
					meshBuilder.Vertices.Add( currentPoint );
					meshBuilder.UVs.Add( Vector3.back );
					meshBuilder.AddTriangle( 0, i - 1, i );
				}
				else if ( i == 0 || i == 1 )
				{
					meshBuilder.Vertices.Add( currentPoint );
					meshBuilder.UVs.Add( Vector3.back );
				}
				currentPoint = angleToVector( currentAngle );
				currentAngle += ( 2 * Mathf.PI / 5 );
			}
			mesh = meshBuilder.CreateMesh();
			filter.sharedMesh = mesh;
		}

		private void Shex ()
		{
			currentAngle = Mathf.PI / 3f;
			meshBuilder = new MeshBuilder();
			filter = GetComponent<MeshFilter>();
			filter.sharedMesh = null;
			mesh = new Mesh();
			Vector3 currentPoint = Vector3.zero;
			for ( int i = 0; i <= 7; i++ )
			{
				if ( i == 7 )
				{
					meshBuilder.AddTriangle( 0, i - 1, 1 );
				}
				else if ( i > 1 )
				{
					meshBuilder.Vertices.Add( currentPoint );
					meshBuilder.UVs.Add( Vector3.back );
					meshBuilder.AddTriangle( 0, i - 1, i );
				}
				else if ( i == 0 || i == 1 )
				{
					meshBuilder.Vertices.Add( currentPoint );
					meshBuilder.UVs.Add( Vector3.back );
				}
				currentPoint = angleToVector( currentAngle );
				currentAngle += Mathf.PI / 3f;
			}
			mesh = meshBuilder.CreateMesh();
			filter.sharedMesh = mesh;
		}

		private Vector3 nextVector ( )
		{
			currentAngle += angle;
			Vector3 newVec = angleToVector( currentAngle );
			Debug.Log( newVec );
			return newVec;
		}
		
		private void Pentagon ()
		{
			/*
				r = s / ( 2 * sin(pi/5) )
				h = r * ( 1 + cos( pi/5 ) )
				k = h - r;
				c = h / ( cos ( pi / 10 ) )
				a = 2.5 * s * k
				p = 5s
			*/
			//angle of center point of triangles, five triangle
			float angle = ( 2 * Mathf.PI ) / 5;
			float otherAngle = ( ( 2 * Mathf.PI ) - angle ) * 0.5f;
			Vector3 vec = angleToVector( otherAngle );
			vec.Normalize();
			float piBy5 = Mathf.PI / 5;
			float radius = length / ( 2 * Mathf.Sin( piBy5) );
			float height = radius * ( 1 + Mathf.Cos ( piBy5 ) );
			float halfH = height * 0.5f;
			Vector3 [] points = new Vector3[6];
			//center
			points[0] = Vector3.zero;
			//top
			points[1] = points[0] + vec * halfH;
			//clock wise
			points[2] = points[1]  + rotateVector( ref vec, -( Mathf.PI/4) ) * length;
			points[3] = points[2] + rotateVector( ref vec, -( otherAngle * 2 ) ) * length;
			points[4] = points[3] + rotateVector( ref vec, -( otherAngle * 2 ) ) * length;
			points[5] = points[4] + rotateVector( ref vec, -( otherAngle * 2 ) ) * length;
			
			MeshBuilder meshBuilder = new MeshBuilder();
			MeshFilter filter = GetComponent<MeshFilter>();
			filter.sharedMesh = null;
			Mesh mesh = new Mesh();
			
			foreach ( Vector3 v in points )
			{
				meshBuilder.Vertices.Add( v );
			}
			meshBuilder.AddUVS( Vector3.back, Vector3.back, Vector3.back, Vector3.back, Vector3.back, Vector3.back );
			
			int center = 0;
			for ( int i = 0; i < 5; i++ )
			{
				int last = 0;
				if ( i < 4 )
				{
					last = i + 1;
				}
				else
				{
					last = 1;
				}
				meshBuilder.AddTriangle( center, i, last );
			}
			mesh = meshBuilder.CreateMesh();
			filter.sharedMesh = mesh;
		}

		private IEnumerator SlowBuild ()
		{
			float angle = ( 2 * Mathf.PI ) / 5;
			float otherAngle = ( ( 2 * Mathf.PI ) - angle ) * 0.5f;
			Vector3 vec = angleToVector( otherAngle );
			vec.Normalize();
			float piBy5 = Mathf.PI / 5;
			float radius = length / ( 2 * Mathf.Sin( piBy5) );
			float height = radius * ( 1 + Mathf.Cos ( piBy5 ) );
			float halfH = height * 0.5f;
			Vector3 [] points = new Vector3[6];
			//center
			points[0] = Vector3.zero;
			//top
			points[1] = points[0] + vec * halfH;
			//clock wise
			points[2] = points[1]  + rotateVector( ref vec, -( Mathf.PI/4) ) * length;
			points[3] = points[2] + rotateVector( ref vec, -( otherAngle * 2 ) ) * length;
			points[4] = points[3] + rotateVector( ref vec, -( otherAngle * 2 ) ) * length;
			points[5] = points[4] + rotateVector( ref vec, -( otherAngle * 2 ) ) * length;
			
			MeshBuilder meshBuilder = new MeshBuilder();
			MeshFilter filter = GetComponent<MeshFilter>();
			filter.sharedMesh = null;
			Mesh mesh = new Mesh();
			int i = 0;
			foreach ( Vector3 v in points )
			{
				meshBuilder.Vertices.Add( points[i] );
				meshBuilder.UVs.Add( Vector3.back );
				Debug.Log("i "+i);
				if ( i % 2 == 0 && i != 0 )
				{
					meshBuilder.AddTriangle( 0, i - 1, i );
					mesh = meshBuilder.CreateMesh();
					filter.sharedMesh = mesh;
					yield return new WaitForSeconds(0.5f);
					StartCoroutine( SlowBuild() );
				}
				i++;

			}
			//meshBuilder.AddUVS( Vector3.back, Vector3.back, Vector3.back, Vector3.back, Vector3.back, Vector3.back );
			
			//int center = 0;
			/*
			for ( int i = 0; i < 5; i++ )
			{
				int last = 0;
				if ( i < 4 )
				{
					last = i + 1;
				}
				else
				{
					last = 1;
				}
				meshBuilder.AddTriangle( center, i, last );
			}*/
			mesh = meshBuilder.CreateMesh();
			filter.sharedMesh = mesh;
		}
		
		private Vector3 angleToVector ( float angle )
		{
			//Debug.LogFormat("C: {0} -- S: {1} <== {2}",Mathf.Cos (angle), Mathf.Sin( angle ), angle * Mathf.Rad2Deg );
			return new Vector3( Mathf.Cos (angle) , Mathf.Sin( angle ) );
		}

		private Vector3 rotate (ref Vector3 vec )
		{
			return vec + Vector3.one;
		}
		
		private Vector3 rotateVector ( ref Vector3 v, float angle )
		{
			float c = Mathf.Cos( angle );
			float s = Mathf.Sin( angle );
			v.Normalize();
			float x = v.x * c - v.y * s;
			float y = v.x * s + c * v.y;
			//Debug.Log ( v+" "+angle );
			v = new Vector3( x, y );
			v.Normalize();
			Debug.Log ( v );
			return v;
		}
		
		void BuildQuad(MeshBuilder meshBuilder)
		{
			Vector3 offset = new Vector3(0, 0, 0);
			meshBuilder.Vertices.Add(new Vector3(0.0f, 0.0f, 0.0f) + offset);
			meshBuilder.UVs.Add(new Vector2(0.0f, 0.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			meshBuilder.Vertices.Add(new Vector3(0.0f, length) + offset);
			meshBuilder.UVs.Add(new Vector2(0.0f, 1.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			meshBuilder.Vertices.Add(new Vector3(length, length) + offset);
			meshBuilder.UVs.Add(new Vector2(1.0f, 1.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			meshBuilder.Vertices.Add(new Vector3(length, 0.0f, 0.0f) + offset);
			meshBuilder.UVs.Add(new Vector2(1.0f, 0.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			int baseIndex = meshBuilder.Vertices.Count - 4;
			
			meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);
			meshBuilder.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);
		}

		void BuildQuad(MeshBuilder meshBuilder, Vector3 center)
		{
			float diagonalLength = ( Vector3.one ).magnitude;
			Vector3 bottomLeft = Vector3.zero - Vector3.one * ( 0.5f );
			Vector3 offset = bottomLeft;
			meshBuilder.Vertices.Add(offset);
			meshBuilder.UVs.Add(new Vector2(0.0f, 0.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			meshBuilder.Vertices.Add( new Vector3(0.0f, length) + offset );
			meshBuilder.UVs.Add(new Vector2(0.0f, 1.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			meshBuilder.Vertices.Add(new Vector3(length, length) + offset);
			meshBuilder.UVs.Add(new Vector2(1.0f, 1.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			meshBuilder.Vertices.Add(new Vector3(length, 0.0f, 0.0f) + offset);
			meshBuilder.UVs.Add(new Vector2(1.0f, 0.0f));
			meshBuilder.Normals.Add(Vector3.back);
			
			int baseIndex = meshBuilder.Vertices.Count - 4;
			
			meshBuilder.AddTriangle(baseIndex, baseIndex + 1, baseIndex + 2);
			meshBuilder.AddTriangle(baseIndex, baseIndex + 2, baseIndex + 3);
		}

        #endregion
    }

}
