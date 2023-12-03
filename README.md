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

# Textures
    ITextureService/Cache
        Can load a tetxure from file or from cache based on name

# Sprite rendering
    SpriteRenderSystem
        Position, Texture (atlas), TextureCoords

    AnimationSystem
        TextureCoords, Animation (frames, counter)

# UI
    Html file defines an area
    Entity-component for rendering an area

# DEBUG
    Write debug text wherever without ui areas
        _logger->DebugField("Position: {X}", position.X);
            Collects entries, renders at render time if debug overlay is enabled 
    Define debug ui areas (html files)
