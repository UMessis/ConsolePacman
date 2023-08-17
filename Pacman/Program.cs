using Engine = UMeEngine.UMeEngine;

Engine.Setup(30);
Engine.Start();

while(Engine.IsPlaying)
{
    Engine.Update();
}

Engine.Quit();