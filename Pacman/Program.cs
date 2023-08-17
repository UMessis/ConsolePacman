using Engine = UMeEngine.UMeEngine;

Engine.Start();

while(Engine.IsPlaying)
{
    Engine.Update();
}

Engine.Quit();