Interactive Gameplay Systems for Unity (URP)
This repository contains a collection of decoupled, event-driven interaction systems developed for Unity. The focus was on creating a seamless blend of visual feedback, spatial audio, and cinematic camera transitions while maintaining a clean project architecture.

Core Technical Implementations
Dynamic Interaction & Visual Feedback
Self-Configuring Outlines: Developed a script that automatically handles Outline component injection at runtime. This eliminates manual setup errors and ensures compatibility with the Universal Render Pipeline (URP).

World-Space UI Transitions: Integrated a proximity-based UI system for object interaction (Letters/Pickups). It utilizes CanvasGroup alpha blending and RectTransform offsets to create a non-intrusive HUD experience.

Immersive Collection Logic
Asynchronous Cleanup: Implemented a mushroom collection sequence using C# Coroutines. The system synchronizes VFX spawning and spatial audio playback with a procedural "shrink-and-destroy" animation to ensure smooth object pooling and memory management.

Input-Responsive Audio Environment
State-Aware Audio Feedback: Rather than simple trigger-based loops, the ladder climbing audio tracks the player's actual movement. By gating audio playback behind Input.GetAxis thresholds, the system ensures the sound stops and starts in perfect sync with the character's physical displacement.

Guided Cinematic Framing
Dynamic Focal Transitions: Created a "Cutscene Camera" trigger that overrides the standard third-person follow-cam. Using Mathf.SmoothStep for positional and rotational interpolation, the camera provides a cinematic focus on specific environmental cues (like doors opening) before returning control to the player.

Development Insights (Refactoring & Debugging)
During development, I prioritized system stability and performance:

Event-Driven Optimization: Transitioned from polling-based logic to an event-driven approach for climbing mechanics, significantly reducing unnecessary Update calls.
Conflict Resolution: Debugged and resolved a "No Receiver" exception within the Animation Event pipeline by centralizing audio triggers into a dedicated PlayerAudioManager component.
VCS Management: Maintained a lightweight repository by implementing a strict .gitignore policy, filtering out Unity's redundant Library and Temp metadata while preserving project integrity.

Technical Specifications
Engine: Unity 2022.3 LTS
Workflow: Universal Render Pipeline (URP)
Architecture: Component-based / Decoupled C# Scripts
