# 🛺 Chole Amar Rickshaw

[![Play on itch.io](https://img.shields.io/badge/Play-itch.io-FA5C5C?style=for-the-badge&logo=itch.io&logoColor=white)](https://enigmah-00.itch.io/chole-amar-ricksaw)
[![Unity](https://img.shields.io/badge/Unity-6-black?style=for-the-badge&logo=unity)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-blue?style=for-the-badge)](LICENSE)

> *Navigate the chaotic streets in this adrenaline-pumping 3D endless runner!*

Experience the thrill of driving a rickshaw through endless traffic, dodging obstacles, and racing against time in this fast-paced arcade game.

---

## 🎮 Play Now

**► [Play Chole Amar Rickshaw on itch.io](https://enigmah-00.itch.io/chole-amar-ricksaw)** ◄

Available for **Windows**, **macOS**, **Linux**, and **Android** (WebGL coming soon!)

---

## 🌟 Features

### 🚗 **Physics-Based Driving**
- Realistic vehicle physics with momentum-based steering
- Smooth acceleration, braking, and drift mechanics
- Dynamic collision detection and ragdoll destruction

### 🤖 **Intelligent AI Traffic**
- Smart AI cars with lane management and obstacle avoidance
- Automatic horn alerts when cars get too close
- Object pooling system for optimized performance

### 💥 **Epic Crash System**
- Spectacular explosion effects with physics-based destruction
- Slow-motion impact sequences for maximum drama
- Car fragments with individual rigidbody physics

### 🎵 **Immersive Audio**
- Dynamic engine sounds that respond to speed
- Realistic tire skid and crash sound effects
- Player-controlled horn (H key) and AI honking
- Atmospheric background music

### 🎯 **Cross-Platform Controls**
- **PC**: Full keyboard/gamepad support
- **Mobile**: Intuitive touch controls with gesture recognition
- Seamless input switching between platforms

---

## 🕹️ Controls

### **Keyboard (PC/Mac/Linux)**
| Key | Action |
|-----|--------|
| **↑ / W** | Accelerate Forward |
| **↓ / S** | Brake / Reverse |
| **← / A** | Steer Left |
| **→ / D** | Steer Right |
| **H** | Horn |
| **R** | Restart (after crash) |

### **Touch Controls (Android/Mobile)**
| Gesture | Action |
|---------|--------|
| **Touch & Hold** | Auto-accelerate + Steer |
| **Touch Left Side** | Steer Left |
| **Touch Right Side** | Steer Right |
| **Touch Center** | Go Straight |
| **Swipe Down** | Brake |
| **Two-Finger Tap** | Horn |

### **Gamepad Support**
- Full controller support with analog stick steering
- Button mapping follows standard gamepad layouts

---

## 🎯 How to Play

1. **Avoid Traffic** - Navigate through lanes of AI-controlled vehicles
2. **Dodge Obstacles** - Watch out for road hazards and barriers
3. **Survive as Long as Possible** - The longer you last, the higher your score
4. **Stay Alert** - Collisions result in spectacular crashes and game over
5. **Master the Controls** - Smooth steering is key to survival

---

## 🛠️ Technical Details

### **Built With**
- **Engine**: Unity 6
- **Language**: C#
- **Input System**: Unity's New Input System
- **Physics**: Custom Rigidbody-based vehicle controller
- **Audio**: Unity AudioSource with dynamic mixing
- **Version Control**: Git + GitHub

### **Key Systems**
- **Vehicle Physics**: Velocity-based movement with linear damping
- **AI System**: Raycasting-based obstacle detection with lane switching
- **Collision Detection**: Tag-based filtering with root GameObject fallback
- **Time Manipulation**: Coroutine-driven slow-motion effects
- **Object Pooling**: Efficient AI car spawning and recycling
- **Audio Management**: Pitch-shifted engine sounds based on speed

### **Performance Optimizations**
- Object pooling for AI traffic (20 car pool, spawns at +100 units)
- Mesh collider optimization with capsule colliders for vehicles
- Coroutine-based updates for less critical systems
- LayerMask filtering for targeted raycasting

---

## 📦 Installation

### **Play in Browser**
Simply click the [itch.io link](https://enigmah-00.itch.io/chole-amar-ricksaw) and hit play!

### **Download for Desktop**

#### **Windows**
1. Download the `.zip` file from itch.io
2. Extract all files to a folder
3. Run `CholeAmarRickshaw.exe`

#### **macOS**
1. Download the `.zip` file
2. Extract and locate `CholeAmarRickshaw.app`
3. Right-click → "Open" (first time only)
4. Click "Open" in security dialog

#### **Linux**
1. Download the `.zip` file
2. Extract files
3. Open terminal in extracted folder:
   ```bash
   chmod +x CholeAmarRickshaw.x86_64
   ./CholeAmarRickshaw.x86_64
   ```

### **System Requirements**
- **OS**: Windows 10+, macOS 10.13+, Ubuntu 18.04+, Android 7.0+
- **Graphics**: DirectX 11 or OpenGL 4.5 compatible
- **Storage**: 500 MB available space
- **Memory**: 2 GB RAM

---

## 🎨 Features in Detail

### **Collision System**
- **Player vs Obstacle**: Instant explosion
- **Player vs AI Car**: Both vehicles explode
- **AI vs AI**: Pass through (no collision to prevent traffic jams)
- **Ground Detection**: Proper physics grounding without explosion

### **AI Behavior**
- Random lane selection on spawn
- Speed variation (2-4 units) for traffic diversity
- Forward raycasting (2 unit range) to detect obstacles
- Automatic honking when car ahead is within 10 units
- Pitch-randomized horn sounds (0.5-1.1x)

### **Explosion Mechanics**
- Car breaks into multiple pieces with individual rigidbodies
- Upward force (200 units) + velocity-based directional force (45x)
- Random torque for realistic tumbling
- Pieces tagged as "CarPart" for cleanup
- Time scale manipulation (1.0 → 0.2 → 1.0 over ~1 second)

---

## 🚀 Development

### **Project Structure**
```
Assets/
├── Scenes/          # Game scenes
├── Scripts/
│   ├── Car/         # Vehicle controller
│   ├── AI/          # AI traffic system
│   ├── Input/       # Input handling
│   ├── Explode/     # Destruction system
│   └── UI/          # UI management
├── Prefabs/         # Reusable game objects
├── Models/          # 3D models
├── Materials/       # Textures and materials
└── SFX/             # Sound effects and music
```

### **Core Scripts**
- `CarHandler.cs` - Vehicle physics and input processing
- `AIHandler.cs` - AI traffic behavior and collision detection
- `InputHandler.cs` - Cross-platform input management
- `ExplodeHandler.cs` - Destruction and explosion effects
- `AICarSpawner.cs` - Object pooling and traffic management
- `UIHandler.cs` - UI updates and game state

---

## 👥 Contributors

**Development Team**
- **Enigmah-00** - Lead Developer
- **ZoayriaAbedin** - Developer

---

## 📝 Version History

### **v1.0.0** (Current)
- ✅ Initial release with full gameplay
- ✅ PC and mobile controls
- ✅ AI traffic system
- ✅ Explosion mechanics
- ✅ Audio implementation
- ✅ Cross-platform support

### **Planned Features**
- 🔄 Power-ups and collectibles
- 🔄 Multiple rickshaw models
- 🔄 Day/night cycle
- 🔄 Weather effects
- 🔄 Leaderboard system
- 🔄 Achievement system

---

## 🐛 Known Issues

- Mobile restart requires game reload (no UI button yet)
- AI cars may occasionally clip through obstacles at high speeds
- Engine sound may stutter during rapid speed changes

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🔗 Links

- **🎮 Play Game**: [itch.io Page](https://enigmah-00.itch.io/chole-amar-ricksaw)
- **💻 Source Code**: [GitHub Repository](https://github.com/Enigmah-00/CholeAmarRicksaw)
- **🐛 Report Issues**: [GitHub Issues](https://github.com/Enigmah-00/CholeAmarRicksaw/issues)

---

## 💬 Feedback & Support

Enjoying the game? Leave a rating on [itch.io](https://enigmah-00.itch.io/chole-amar-ricksaw)!

Found a bug? [Open an issue](https://github.com/Enigmah-00/CholeAmarRicksaw/issues) on GitHub.

---

<div align="center">

**Made with ❤️ in Unity**

*Drive safe, drive fast, dodge traffic!* 🛺💨

[⭐ Star this repo](https://github.com/Enigmah-00/CholeAmarRicksaw) | [🎮 Play Now](https://enigmah-00.itch.io/chole-amar-ricksaw) | [🐛 Report Bug](https://github.com/Enigmah-00/CholeAmarRicksaw/issues)

</div>
