using Pacman;

UMeEngine.UMeEngine engine = new UMeEngine.UMeEngine(30);

engine.Start();

while(engine.IsPlaying)
{
    engine.Update();
}

engine.Quit();