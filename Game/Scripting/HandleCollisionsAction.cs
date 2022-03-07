using System;
using System.Collections.Generic;
using System.Data;
using Unit05.Game.Casting;
using Unit05.Game.Services;


namespace Unit05.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false)
            {
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Timer time = new Timer();
            Snake1 snake1 = (Snake1)cast.GetFirstActor("snake1");
            Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
            Actor head1 = snake1.GetHead();
            Actor head2 = snake2.GetHead();
            List<Actor> body1 = snake1.GetBody();
            List<Actor> body2 = snake2.GetBody();
            snake1.GrowTail(time.GetPoints());
            snake2.GrowTail(time.GetPoints());
            foreach (Actor segment in body1)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()) || segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                }
            }
            foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()) || segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (isGameOver == true)
            {
                Snake1 snake1 = (Snake1)cast.GetFirstActor("snake1");
                Snake2 snake2 = (Snake2)cast.GetFirstActor("snake2");
                List<Actor> segments1 = snake1.GetSegments();
                List<Actor> segments2 = snake2.GetSegments();

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments1)
                {
                    segment.SetColor(Constants.WHITE);
                }
                foreach (Actor segment in segments2)
                {
                    segment.SetColor(Constants.WHITE);
                }
            }
        }

    }
}