# Entry point (Program.cs) creates a host builder
* Registers Nord Engine top level (root) dependencies 
  * Including TApplication
* Scans registers adds all ISceneCompositionRoot<TScene> into separate child containers, one per TScene
  * Registers ECS world (one per scene), systems, scene-scoped processes and bus
* Runs the application object
  * Application object pushes a scene
    * Scene service uses scene factory to get the scene child container and composes the root, resolving the scene
      * Default application base runs event loop
        * Updates global processes 
        * Updates the current scene
          * The current scene updates its world/systems/processes
        * When a scene is popped, it is disposed

# DI
## Root container
Contains global bus, global processes, resource caches

## Scene container (child container) 
Contains scoped bus, scoped processes, scoped input maps, scoped systems etc
Can request a global container, global bus

# TODO
* Physics
  * Bounding boxes
  * SAT+gravity enough? (Arcade physics)
  * Lib for kinematic physics?
  * Lighting? normals
* UI
  * Ultralight
  * Console, commands
* Viewports (camera)
* Net code
* Sound, streams
* Particles
* Shaders
* Debug
  * Bounding boxes, entities, trees
* Tile maps (TileD)