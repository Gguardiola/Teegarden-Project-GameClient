# Dreyfus - GameClient
![GitHub release (latest by date)](https://img.shields.io/github/v/release/Gguardiola/Teegarden-Project-GameClient?style=flat-square)
![Dreyfus Banner](resources/misc/dreyfusBanner.png)
## Overview
**Dreyfus GameClient (codename Teegarden Project)** is my **_final degree project_**, which consists of developing a cloud-based distributed system that integrates a video game (also developed from scratch) where the final enemy is controlled by an artificial intelligence model trained using supervised learning and reinforcement learning methods.

This enemy is capable of progressively learning, since each combat is recorded and sent to the cloud server (infrastructure) to train a new model that is later downloaded by the GameClient, creating a continuous cycle in which the AI becomes increasingly intelligent.

The system is composed of three main elements:

- **Dreyfus GameClient** (this repository)

- **Teegarden-Project-API-Infra:** https://github.com/Gguardiola/Teegarden-Project-API-Infra
- **Teegarden-Project-RL-Trainer:** https://github.com/Gguardiola/Teegarden-Project-Intellicombat-RL-Trainer
## Key Features
- **Cloud Integration**: Seamlessly connects to a cloud server for data exchange and model updates
- **AI Opponent**: Features an enemy controlled by an AI model that learns and adapts over time
- **Data Collection**: Records gameplay data to improve the AI model through supervised and reinforcement learning
- **FPS Gameplay**: Engaging first-person shooter experience
- **Procedural Generation**: Levels are procedurally generated for unique gameplay experiences

## Note
This project is a prototype and is not intended for commercial use. It is developed for educational purposes as part of my final degree project.