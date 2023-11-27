Push<MyScene>()
    container = MySceneContext.Build()
    SceneFactory.Build<MyScene>(container)
    MyScene.Create()

Pop()
    CurrentScene.Destroy()
    SceneContainer.Dispose()?

Scenes are entity worlds?

Application.Run()
    ChangeScene<MyScene>()
    Loop
        Update systems