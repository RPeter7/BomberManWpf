using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BomberMan.Logic.Feature.GameModel;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.View.Feature.Game.GameDisplay
{
    public class GameDisplay : FrameworkElement
    {
        public GameDisplay()
        {
            Messenger.Default.Register<NotificationMessage<bool>>(this, x => Application.Current?.Dispatcher?.Invoke(InvalidateVisual));
        }

        public GameModel GameModel
        {
            get => (GameModel)GetValue(GameModelProperty);
            set => SetValue(GameModelProperty, value);
        }

        public static readonly DependencyProperty GameModelProperty =
            DependencyProperty.Register(nameof(GameModel), typeof(GameModel), typeof(GameDisplay), new PropertyMetadata(null));

        public ImageBrush GetBrush(string filename) => new ImageBrush(new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute)));

        protected override void OnRender(DrawingContext drawingContext)
        {
            var elementWidth = ActualWidth / GameModel.GetMap.GetLength(1);
            var elementHeight = ActualHeight / GameModel.GetMap.GetLength(0);

            for (var i = 0; i < GameModel.GetMap.GetLength(0); i++)
            {
                for (var j = 0; j < GameModel.GetMap.GetLength(1); j++)
                {
                    var mapElement = GameModel.GetMap[i, j];
                    switch (mapElement.MapElement)
                    {
                        case BreakableWall breakableWall:
                            drawingContext.DrawRectangle(GetBrush(breakableWall.PathOfImage), new Pen(Brushes.Black, 0), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                            break;
                        case EmptyField emptyField:
                            drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.Black, 1), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                            break;
                        case NonBreakableWall nonBreakableWall:
                            drawingContext.DrawRectangle(GetBrush(nonBreakableWall.PathOfImage), new Pen(Brushes.Black, 0), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                            break;
                        case PlayerDto playerDto:
                            drawingContext.DrawRectangle(GetBrush(playerDto.PathOfImage), new Pen(Brushes.Black, 0), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                            break;
                        case NonPlayableCharacter nonPlayableCharacter:
                            drawingContext.DrawRectangle(GetBrush(nonPlayableCharacter.PathOfImage), new Pen(Brushes.Black, 0), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                            break;
                    }

                    if (mapElement.BlowingElement == null) continue;
                    switch (mapElement.BlowingElement)
                    {
                        case Bomb bomb:
                            if (mapElement.MapElement is PlayerDto)
                            {
                                drawingContext.DrawRectangle(GetBrush(IsPlayerOne(mapElement) ? "../../Images/PlayerOneBOMB.png" : "../../Images/PlayerTwoBOMB.png"), new Pen(Brushes.Black, 0), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                                break;
                            }
                            drawingContext.DrawRectangle(GetBrush(bomb.PathOfImage), new Pen(Brushes.Black, 0), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                            break;
                        case Flame flame:
                            drawingContext.DrawRectangle(GetBrush(flame.PathOfImage), new Pen(Brushes.Black, 0), new Rect(j * elementWidth, i * elementHeight, elementWidth, elementHeight));
                            break;
                    }
                }
            }
        }

        private static bool IsPlayerOne(MapElementContainer mapElementContainer) => ((PlayerDto)mapElementContainer.MapElement).EntityType == Data.Enums.EntityType.PlayerOne;
    }
}
