# Stellar League
### by presh (Prashant Naidu)

#### A sandbox Unity project that implements Rocket League esque aerial mechanics to a space vehicle in space.

#### I'm hoping for folks to use this as a playground to experiment with their own ideas. I've always thought bringing Rocket League aerial mechanics out of the context of Rocket League would be interesting to tackle. Let's see what happens!

## Documentation
Unity 2022.3.10f1

### FlightController
| **Inspector References** | **Description** |
| ------------------------ | ---- |
|   m_Rigidbody            | The vehicle's rigidbody |

| **Surfaced Parameters**         | **Description** |
| ------------------------------- | ---- |
| **m_ThrusterForce**             | Force applied for rotational movement |
| **m_MagnetThrusterForce**       | Force applied for turning while attached to a wall |
| **m_BoosterForce**              | Force applied for forward movement |
| **m_BurstForce**                | Impulse force applied for when performing a Rocket League style dodge |  
| **m_BurstCooldown**             | Cooldown time before being able to use 'Burst' again |
| **m_HyperSpeedBoostMultiplier** | This value multiplied by the m_BoosterForce is the force applied for forward movement during Hyperspeed |
| **m_MagnetForce**               | The force applied to the vehicle towards the "magnetized" wall |

### CameraController
| **Inspector References** | **Description** |
| ------------------------ | ---- |
| **m_TargetToFollow**     | The transform the camera tracks to |
| **m_FlightController**   | A reference to the FlightController |
| **m_TargetToLockOn**     | A transform the camera will always face when in CameraMode.Locked |
| **m_CameraYOffsetGameObject**       | A reference to the CameraYOffset GameObject |

| **Surfaced Parameters**             | **Description** |
| ----------------------------------- | ---- |
| **m_SmoothSpeed**                   | The speed at which the camera Lerps to the m_TargetToFollow.position |
| **m_RotationSpeed**                 | The speed at which the camera Slerps towards the desired rotation |
| **m_CameraMoveFactor**              | A multiplier for how much the camera moves when camera movement is applied (like Rocket League right joystick movement) |
| **m_CameraMode** (enum)             | FORWARD: Camera faces the forward momentum of the m_TargetToFollow, LOCKED: Camera faces the m_TargetToLockOn |
| **m_YOffsetDefaultPosition**        | Default position for the CameraYOffset |
| **m_YOffsetDownwardOffsetPosition** | Lower offset position for the CameraYOffset |
| **m_YOffsetCurve**                  | Animation Curve for the CameraYOffset motion |
| **m_YOffsetSpeed**                  | The speed for the motion animation |

### GameInput

A singleton that handles routing the m_PlayerControls to the rest of the scripts that need to make sense of any player input. m_PlayerControls is an action map setup in /Assets/Controls/PlayerControls using the new Unity Input System. All controls can be modified in the action map. The routing for controls to the rest of the game can be modified in GameInput.cs
