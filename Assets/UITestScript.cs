using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
 
 
[ExecuteInEditMode]
public class UITestScript: Graphic
{
    [Range(0,100)]
    public int fillPercent;
    public bool fill = true;
    public int thickness = 5;
 
    void Update(){
		this.thickness = (int)Mathf.Clamp(this.thickness, 0, rectTransform.rect.width / 2);
    }
 
    protected override void OnFillVBO (List<UIVertex> vbo){
        float outer = -rectTransform.pivot.x * rectTransform.rect.width;
        float inner = -rectTransform.pivot.x * rectTransform.rect.width + this.thickness;
 
        vbo.Clear();
 
        UIVertex vert = UIVertex.simpleVert;
        Vector2 prevX = Vector2.zero;
        Vector2 prevY = Vector2.zero;
 
        float f = (float)(this.fillPercent / 100f);
        int fa = (int)(361 * f);
 
        for (int i = 0; i < fa; ++i) {
            float rad = Mathf.Deg2Rad * i;
            float c = Mathf.Cos(rad);
            float s = Mathf.Sin(rad);
            float x = outer * c;
            float y = inner * c;
            vert.color = color;
            vert.position = prevX;
            vbo.Add(vert);
            prevX = new Vector2(outer * c, outer * s);
            vert.position = prevX;
            vbo.Add(vert);

            if (this.fill) {
                vert.position = Vector2.zero;
                vbo.Add(vert);
                vbo.Add(vert);
            } else {
				const int barHeight = 15;
				const int barSpacing = 10;
				
				float c2 = Mathf.Cos(rad + Mathf.PI / 45.0f);
				float s2 = Mathf.Sin(rad + Mathf.PI / 45.0f);

                vert.position = new Vector2(inner * c, inner * s);
                vbo.Add(vert);
                vert.position = prevY;
                vbo.Add(vert);
                prevY = new Vector2(inner * c2, inner * s2);

				if (i % barSpacing == 0) {
					vert.position = new Vector2((inner + barHeight) * c, (inner + barHeight) * s);
					vbo.Add(vert);
					vert.position = new Vector2((inner + barHeight) * c2, (inner + barHeight) * s2);
					vbo.Add(vert);
					vert.position = prevY;
					vbo.Add(vert);
				}
            }
        }
    }
}
