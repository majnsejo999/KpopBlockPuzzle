using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class UIGradient : BaseMeshEffect
{
    public struct Matrix2x3
    {
        public float m00;
        public float m01;
        public float m02;
        public float m10;
        public float m11;
        public float m12;

        public Matrix2x3(float m00, float m01, float m02, float m10, float m11, float m12)
        {
            this.m00 = m00;
            this.m01 = m01;
            this.m02 = m02;
            this.m10 = m10;
            this.m11 = m11;
            this.m12 = m12;
        }

        public static Vector2 operator *(Matrix2x3 m, Vector2 v)
        {
            float x = (m.m00 * v.x) - (m.m01 * v.y) + m.m02;
            float y = (m.m10 * v.x) + (m.m11 * v.y) + m.m12;
            return new Vector2(x, y);
        }
    }

    public Color color1 = Color.white;
    public Color color2 = Color.white;

    [Range(-180f, 180f)]
    public float angle = 0f;
    public bool ignoreRatio = true;

    public override void ModifyMesh(VertexHelper vh)
    {
        if(enabled)
        {
            Rect rect = graphic.rectTransform.rect;
            Vector2 dir = RotationDir(angle);

            if(!ignoreRatio) dir = CompensateAspectRatio(rect, dir);

            Matrix2x3 localPositionMatrix = LocalPositionMatrix(rect, dir);

            UIVertex vertex = default(UIVertex);
            for(int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                Vector2 localPosition = localPositionMatrix * vertex.position;
                vertex.color *= Color.Lerp(color2, color1, localPosition.y);
                vh.SetUIVertex(vertex, i);
            }
        }
    }

    public Vector2 RotationDir(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);
        return new Vector2(cos, sin);
    }

    public Vector2 CompensateAspectRatio(Rect rect, Vector2 dir)
    {
        float ratio = rect.height / rect.width;
        dir.x *= ratio;
        return dir.normalized;
    }

    public Matrix2x3 LocalPositionMatrix(Rect rect, Vector2 dir)
    {
        float cos = dir.x;
        float sin = dir.y;
        Vector2 rectMin = rect.min;
        Vector2 rectSize = rect.size;
        float c = 0.5f;
        float ax = rectMin.x / rectSize.x + c;
        float ay = rectMin.y / rectSize.y + c;
        float m00 = cos / rectSize.x;
        float m01 = sin / rectSize.y;
        float m02 = -(ax * cos - ay * sin - c);
        float m10 = sin / rectSize.x;
        float m11 = cos / rectSize.y;
        float m12 = -(ax * sin + ay * cos - c);
        return new Matrix2x3(m00, m01, m02, m10, m11, m12);
    }
}