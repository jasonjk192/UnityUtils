using UnityEngine;

namespace WinterCrestal.Extensions.Debug
{
    public static class GizmosExtensions
    {
        public static void DrawBounds(Bounds bounds, Vector3 offset, Quaternion rotation)
        {
            Vector3 ld = new(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
            Vector3 lt = new(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y);
            Vector3 rd = new(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);
            Vector3 rt = new(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y);

            ld = bounds.center + rotation * (ld - bounds.center) + offset;
            lt = bounds.center + rotation * (lt - bounds.center) + offset;
            rd = bounds.center + rotation * (rd - bounds.center) + offset;
            rt = bounds.center + rotation * (rt - bounds.center) + offset;

            Gizmos.DrawLine(ld, lt);
            Gizmos.DrawLine(lt, rt);
            Gizmos.DrawLine(rt, rd);
            Gizmos.DrawLine(rd, ld);
        }

        public static void DrawBounds(Bounds bounds, Transform transform)
        {
            Vector3 ld = new(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);
            Vector3 lt = new(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y);
            Vector3 rd = new(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);
            Vector3 rt = new(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y);

            ld = transform.TransformPoint((ld - bounds.center) * .5f);
            lt = transform.TransformPoint((lt - bounds.center) * .5f);
            rd = transform.TransformPoint((rd - bounds.center) * .5f);
            rt = transform.TransformPoint((rt - bounds.center) * .5f);

            Gizmos.DrawLine(ld, lt);
            Gizmos.DrawLine(lt, rt);
            Gizmos.DrawLine(rt, rd);
            Gizmos.DrawLine(rd, ld);
        }
    }

}

