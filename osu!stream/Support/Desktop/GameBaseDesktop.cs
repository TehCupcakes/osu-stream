using osum.Audio;
using osum.GameModes;
using osum.Input;
using osum.Input.Sources;

namespace osum.Support.Desktop
{
    public class GameBaseDesktop : GameBase
    {
        public GameWindowDesktop Window;

        public GameBaseDesktop(OsuMode mode = OsuMode.Unknown) : base(mode)
        {
        }

        public override void Run()
        {
            Window = new GameWindowDesktop(Config.GetValue("Fullscreen", false), Config.GetValue("WindowWidth", 1280), Config.GetValue("WindowHeight", 720));
            Window.Run();
            Director.CurrentMode.Dispose();
        }

        protected override BackgroundAudioPlayer InitializeBackgroundAudio()
        {
            return new BackgroundAudioPlayerDesktop();
        }

        protected override SoundEffectPlayer InitializeSoundEffects()
        {
            return new SoundEffectPlayerBass();
        }

        protected override void InitializeInput()
        {
            InputSource source;

            try
            {
                source = new InputSourceRaw(Window);
            }
            catch
            {
                source = new InputSourceMouse(Window.Mouse);
            }

            InputManager.AddSource(source);
        }

        public override void SetupScreen()
        {
            NativeSize = Window.ClientSize;

            base.SetupScreen();
        }

        public void Exit()
        {
            Window.Exit();
        }
    }
}