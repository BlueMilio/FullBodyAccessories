// Code modified from tModLoader ExampleMod
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FullBodyAccessories.UI
{
    public class DraggableUIPanel : UIPanel
    {
        private Vector2 _offset;
        
        public bool Dragging { get; private set; }
        
        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);

            if (evt.MousePosition.Y >= Top.Pixels && evt.MousePosition.Y <= (Top.Pixels + PaddingTop))
            {
                DragBegin(evt);
            }
        }

        public override void MouseUp(UIMouseEvent evt)
        {
            base.MouseUp(evt);

            if (Dragging)
                DragEnd(evt);
        }

        private void DragBegin(UIMouseEvent e)
        {
            _offset = new Vector2(e.MousePosition.X - Left.Pixels, e.MousePosition.Y - Top.Pixels);
            Dragging = true;
        }

        private void DragEnd(UIMouseEvent e)
        {
            Vector2 end = e.MousePosition;
            Dragging = false;

            Left.Set(end.X - _offset.X, 0);
            Top.Set(end.Y - _offset.Y, 0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }

            if (Dragging)
            {
                Left.Set(Main.mouseX - _offset.X, 0);
                Top.Set(Main.mouseY - _offset.Y, 0);
            }

            Rectangle parentDimensions = Parent.GetDimensions().ToRectangle();

            if (!GetDimensions().ToRectangle().Intersects(parentDimensions))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentDimensions.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentDimensions.Bottom - Height.Pixels);
            }
        }
    }
}
