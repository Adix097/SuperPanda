# SuperPanda

SuperPanda is a Unity 3D runner prototype in which a panda runs around a circular
track, collects bamboo, and avoids obstacles. The track content becomes more
challenging as the panda completes laps.

## Requirements

- Unity **6000.4.6f1** (Unity 6)
- A desktop target with keyboard support, or a touchscreen-capable target
- Git, if you are cloning or contributing to the project

The project uses the Universal Render Pipeline and Unity's Input System. Unity
will restore the package dependencies listed in `Packages/manifest.json` when the
project is opened.

## Getting started

1. Open Unity Hub.
2. Select **Add > Add project from disk**.
3. Choose the SuperPanda project folder.
4. Open the project with Unity `6000.4.6f1`.
5. Open `Assets/Scenes/MainScene.unity`.
6. Press **Play** to start the game.

The main scene is included in the project's Editor Build Settings, so it can
also be used as the starting scene for a build.

## Controls

### Keyboard

| Action | Input |
| --- | --- |
| Move toward the inside or outside of the track | `A` / `D` or Left / Right Arrow |
| Jump | `Space` |

### Touch

- Tap the screen to jump.

The panda runs automatically. Horizontal movement is limited to the playable
track area, and jumping is only available while grounded.

## Gameplay

- Collect bamboo to increase the score.
- Bamboo is worth more points on later laps because the lap number acts as the
  score multiplier.
- The current lap, score, bamboo count, multiplier, and best score are shown in
  the HUD.
- Hitting an obstacle resets the current run's score and bamboo count.
- The best score is saved using Unity `PlayerPrefs`.
- Each lap regenerates the obstacle layout.
- Early laps use logs; rocks appear from lap 3, and moving obstacles can appear
  from lap 4.

## Project structure

```text
Assets/
├── Panda/          Panda model and animation controller
├── Prefabs/        Reusable track prefabs
├── Scenes/         MainScene.unity
├── Scripts/        Gameplay and UI behaviour
└── Materials/      Project materials
Packages/            Unity package manifest and lock file
ProjectSettings/     Unity editor and player configuration
```

Important scripts include:

- `PandaRunner.cs` - automatic movement, steering, jumping, and collision
  handling.
- `ObstacleSpawner.cs` - procedural obstacle and bamboo placement per lap.
- `ScoreManager.cs` - score, bamboo, multiplier, and best-score persistence.
- `GameHUD.cs` - runtime lap and score display.
- `TrackBuilder.cs` and `LapWorld.cs` - track and lap-world setup.

## Building

1. Open **File > Build Profiles** in Unity.
2. Select the desired target platform.
3. Confirm that `Assets/Scenes/MainScene.unity` is included in the scene list.
4. Select **Build** or **Build and Run**.

Generated Unity folders such as `Library`, `Temp`, `Logs`, and `UserSettings`
are excluded by [`.gitignore`](.gitignore) and should not be committed.

## Development notes

- Make gameplay changes in `Assets/Scripts`.
- Keep Unity-generated `.meta` files alongside their assets.
- Open the project with the specified Unity version to avoid unnecessary asset
  or project-setting changes.