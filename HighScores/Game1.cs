using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighScores
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        HighScoreDataStore _highScores;
        SpriteFont _font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _highScores = new HighScoreDataStore();
            // Task<bool> result = InsertData(); // Only run this once
        }

        public async Task<bool> InsertData()
        {
            try
            {
                await _highScores.AddAsync(new HighScore() { Id = 0, Name = "Jill", Score = 10000 });
                await _highScores.AddAsync(new HighScore() { Id = 1, Name = "Dave", Score = 5000 });
                await _highScores.AddAsync(new HighScore() { Id = 2, Name = "Fred", Score = 9000 });
            }
            catch
            {
                return false;
            }

            return true;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _font = Content.Load<SpriteFont>("fonts\font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Task<List<HighScore>> scores = _highScores.GetAsync(false);
            List<HighScore> sorted = scores.Result.OrderByDescending(o => o.Score).ToList();

            _spriteBatch.Begin();
            Vector2 position = new Vector2();

            foreach (HighScore score in sorted)
            {
                _spriteBatch.DrawString(_font, score.Name + " - " + score.Score, position, Color.White);
                position.Y += _font.LineSpacing;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
