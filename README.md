# Entry point (Program.cs) creates a host builder
    Registers Nord Engine top level (root) dependencies 
        Including TApplication
    Scans registers adds all ISceneCompositionRoot<TScene> into separate child containers, one per TScene
        Registers ECS world (one per scene), systems
    Runs the application object
        Application object pushes a scene
            Scene service uses scene factory to get the scene child container and composes the root, resolving the scene
        Default application base runs event loop
            Updates the current scene
                The current scene updates its world/systems
            When a scene is popped, it is disposed
