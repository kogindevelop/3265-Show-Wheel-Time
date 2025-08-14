using System.Threading;
using Cysharp.Threading.Tasks;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class GameQuizScreenStateController : StateController
    {
        private readonly GameUIContainer _uiContainer;
        
        private GameQuizScreen _screen;
        
        public GameQuizScreenStateController(GameUIContainer uiContainer)
        {
            _uiContainer = uiContainer;
        }
        
        public override async UniTask EnterState()
        {
            _screen = await _uiContainer.CreateScreen<GameQuizScreen>();
        }
        
        public override async UniTask ExitState()
        {
            await _uiContainer.HideScreen<GameQuizScreen>();
        }   
    }
}