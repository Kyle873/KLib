using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KLib
{
    public class Window : Control
    {
        public Color rectColor = Color.White;
        public Color fillColor = Color.White;
        public Texture2D icon = null;
        public SpriteFont font = null;
        public string title = string.Empty;
        public bool titleHovering = false;
        public List<Control> controls = new List<Control>();

        bool wasTitleHovering = false;

        public Window(Vector2 pos, int width, int height, string title = "")
            : base(pos)
        {
            this.position = pos;
            this.title = title;
            this.width = width;
            this.height = height;

            // Default Colors
            this.rectColor = Color.White;
            this.fillColor = Color.Gray;
            this.rectColor.A = 128;
            this.fillColor.A = 128;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (!visible)
                return;
            
            base.Draw(batch);
            
            // Title Bar
            Shape.DrawRect((int)position.X, (int)position.Y, width, 20, rectColor, fillColor);
            
            // Title Text
            if (font != null)
                batch.DrawString(font, title, new Vector2(position.X + 24, position.Y - 1), Color.White);
            
            // Title Icon
            if (icon != null)
            {
                Color iconColor = Color.White;
                iconColor.A = this.fillColor.A;
                batch.Draw(icon, position, iconColor);
            }

            // Window
            Shape.DrawRect((int)position.X, (int)position.Y + 20, width, height - 20, rectColor, fillColor);

            // Draw Controls
            foreach (Control control in controls)
                control.Draw(batch);
        }

        public override void Update(GameTime dt)
        {
            if (!visible)
                return;

            base.Update(dt);

            int x = Input.PrevMouseState.X - Input.MouseState.X;
            int y = Input.PrevMouseState.Y - Input.MouseState.Y;

            // Is the cursor hovering over the title?
            if (Input.MouseX >= position.X &&
                Input.MouseX <= position.X + width &&
                Input.MouseY >= position.Y &&
                Input.MouseY <= position.Y + 20)
                titleHovering = true;
            else
                titleHovering = false;

            // Window Moving
            // TODO: Relative movement
            if ((titleHovering || wasTitleHovering) && Input.MouseState.LeftButton == ButtonState.Pressed)
            {
                wasTitleHovering = true;
                position.X = Input.MouseX;
                position.Y = Input.MouseY;
            }
            else
                wasTitleHovering = false;

            // Clamp window to screen
            if (position.X < 0)
                position.X = 0;
            if (position.X > Engine.ScreenWidth - width)
                position.X = Engine.ScreenWidth - width;
            if (position.Y < 0)
                position.Y = 0;
            if (position.Y > Engine.ScreenHeight - height)
                position.Y = Engine.ScreenHeight - height;

            // Update Controls
            foreach (Control control in controls)
                control.Update(dt);
        }
    }
}
