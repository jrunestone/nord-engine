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
* Contains global bus, global processes, resource caches

## Scene container (child container) 
* Contains scoped bus, scoped processes, scoped input maps, scoped systems etc
* Can request a global container, global bus

# Rendering
* All SpriteComponent are rendered to the main render target (render texture) and then to the window
* Rendering is sorted by by RenderLayerComponent(number) and then by texture
* Custom RenderTargetComponent(id) can be used (render to this, then to the main render target and/or then to the window)
* The rendering targets use the global camera when rendering to the window
* A custom CameraComponent can be used to render a custom view before rendering to the main rendering target and window

# Input
## Action maps
* Set active input action map
* Subscribe via scene-scoped bus to input action events

## Raw input events
* Subscribe via scene-scoped bus to raw input events

# UI

# TODO
* Rendering
  * Entity builder
  * Instancing/batching (tile maps, particles, ...)
* UI
  * Ultralight
  * Console, commands
* View system
  * World view, UI view (layer/camera)
  * UI render texture
  * Transitions when show/hide an overlay
  * Pause when showing overlay
  * Global camera (top-down, side scrolling, isometric, etc)
  * Use different camera (view) when rendering to a render target (ui, minimap, etc)
* Physics
  * Bounding boxes
  * SAT+gravity enough? (Arcade physics)
  * Lib for kinematic physics?
  * Lighting? normals
* Debug
  * Bounding boxes, entities, trees
* Tile maps (TileD)
* Net code
* Sound, streams
* Particles
* Shaders

Entity
  UiComponent(string html/string filename)
  RenderTargetComponent(Targets.UI)
  RenderLayerComponent(Layers.UI)
  CameraComponent()
Events/commands
UiSystem
  Render to ui render texture with ui view
