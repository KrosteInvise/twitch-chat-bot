# 🎮 Twitch Chatbot for Unity
*A small pet project exploring real‑time interaction between Twitch chat and Unity.*

---

## 🚀 Overview
This project is a lightweight Twitch chatbot built in Unity.
Its purpose is to experiment with interactive gameplay mechanics driven by Twitch chat messages — from simple commands to more complex viewer‑driven events.

The project is not intended for production use. It’s a personal sandbox for learning, prototyping, and having fun with Twitch API integrations.

---

## ✨ Features
- 🔌 **Twitch IRC connection** — connects to Twitch chat using an OAuth token
- 💬 **Real‑time message parsing** — listens to chat messages and commands
- 🎮 **Unity event triggers** — chat messages can influence objects or gameplay
- 🧩 **Modular architecture** — easy to extend with new commands or behaviors
- 🧪 **Pet‑project friendly** — minimal setup, readable structure, and room to grow

---

## 🛠️ Tech Stack
- **Unity 6** (6000.0.61f1)
- **C#**
- **TwitchLib**
- **Extenject** (Zenject)
- **Newtonsoft.Json**

---

## 🗄️ Future Backend Plans  
In the long run, this project will include a small backend service responsible for storing player data and handling persistent features.

### Planned backend stack  
- **Java**  
- **Spring** (for REST API and application structure)  
- **PostgreSQL** (for storing player profiles, stats, and progression)  
- **Docker** (mainly for local development and running PostgreSQL in a clean, reproducible environment)

### Deployment direction  
For hosting the backend, the idea is to eventually deploy it to a cloud platform.
The exact provider is still open, but a managed environment — such as a cloud service that supports Java applications and managed databases — would simplify deployment and scaling compared to running everything manually and good for practicing.
