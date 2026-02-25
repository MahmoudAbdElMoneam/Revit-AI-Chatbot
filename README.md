# 🤖 Revit AI Chatbot

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET Framework](https://img.shields.io/badge/.NET_Framework-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Revit](https://img.shields.io/badge/Revit_API-007ACC?style=for-the-badge&logo=autodesk&logoColor=white)
![AI](https://img.shields.io/badge/AI-Powered_by_OLLAMA-blueviolet?style=for-the-badge)
<img width="2379" height="1213" alt="image" src="https://github.com/user-attachments/assets/2841e9f8-c8a8-4347-8676-f65ad3888d40" />

---

## 🧩 Overview

**Revit AI Chatbot** is an intelligent assistant that communicates with **Ollama** supporting **local AI models interactions**, ensuring **data privacy, and offline capability**.
This allows you to experiment freely what can be done with AI in Revit. However, if you run the AI models locally, you will be limited with your GPU capabilities.

> ⚙️ Built for Revit developers who want to interact with BIM data conversationally without compromising confidentiality.

---

## 🚧 Project Status

🚀 *Work in Progress* — expect updates and lots of improvements ahead!  
The current version can:

- 🧠 **Generate and execute code** to query information from the active **Revit Document**.  
- 💬 **Analyze results** and craft intelligent responses to user queries.  
- 🔁 **Handle exceptions dynamically** — just send them to the model again, and it will attempt to fix or rewrite the code.

---

## 🧱 Features

- ✅ Local AI integration via **OLLAMA**
- ✅ Privacy-focused (no cloud data exposure)
- ✅ Editable **system prompts** for controlling generation behavior
- ✅ Code execution from chat context
- 🔄 Self-recovery from exceptions or code errors

---

## ⚙️ How It Works

1. ✍️ You ask the chatbot something about the open Revit model — e.g., *“List all walls higher than 3 meters.”*
2. 🧩 The assistant generates the necessary **C# snippet** to query Revit through the API.
3. ⚡ The code is **executed locally**, and results are passed back to the AI.
4. 🤖 The AI **analyzes and explains** the output conversationally.

---

## 🔧 Customization

The **system prompts** guiding code generation are **fully exposed**.  
You can edit, refine, or completely replace them to tailor how the AI behaves.

> 💡 This allows you to experiment with coding style, completion behavior, or debugging flow.

---

## 🐞 Handling Errors

If an exception occurs during execution:
1. Copy or capture the error.
2. Send it back to the chatbot.
3. The model will **try to fix the code automatically**.

---

## 🛠️ Requirements

- Autodesk Revit
- .NET Framework 4.8
- OLLAMA installed locally (Download and extract ollama-windows-amd64.zip from https://github.com/ollama/ollama/releases, then in the Add-In Browse for the exe file)
- A supported local model from https://ollama.com/search (e.g., **gpt-oss:20b**, **danielsheep/Qwen3-Coder-30B-A3B-Instruct-1M-Unsloth:UD-Q4_K_XL**, etc.)

---

## 📦 Installation for developers

```bash
git clone https://github.com/MahmoudAbdElMoneam/AIChat.git
```
Then open the project in Visual Studio, restore dependencies, and build the solution.
Load the resulting add-in into Revit and start chatting!

---

## 📦 Installation for users

From the releases section on the right, download the zip file, copy to C:\ProgramData\Autodesk\ApplicationPlugins\
Unblock it, extract it to the folder directly.
Then open Revit and start chatting!

🗺️ Roadmap
- More testing on different use-cases
- Enhanced context awareness after closing/reopening
- Fine-tuned local model integration
- Persistent chat history
- Plugin settings UI
- Advanced error tracing
- Handle Delete/ctrl+c/ctrl+v keys presses resulting in losing focus to Revit (Modeless form)
- Allow for different system prompts to be saved and utilized
- Test and enable attachements processing for multimodal interaction

🤝 Contributing
Contributions are welcome!
Feel free to fork this repo, tweak the prompts, or suggest ideas via pull requests or issues.

📜 License
This project is released under the MIT License — use it freely.

🌟 If you find it useful, give it a ⭐ and share feedback!
