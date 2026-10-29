# dn 🚀

`dn` is an ultra-fast, cross-platform CLI automation tool written in modern .NET to bootstrap structured clean-architecture backend workspaces instantly. In a single command, it generates a full architectural foundation complete with an API layout, core libraries, an integrated solution file, and an initialized Git repository.

---

## 🛠️ Architecture Blueprint

When you run `dn`, it safely isolates and spins up the following infrastructure in your workspace:

```text
📂 [YourProjectName]/
 ┣ 📂 [YourProjectName].Api/            (ASP.NET Core Web API template)
 ┣ 📂 [YourProjectName].Infrastructure/ (Data access & infrastructure layer)
 ┣ 📂 [YourProjectName].Domain/         (Core domain models & business logic)
 ┣ 📂 [YourProjectName].Hub/            (Real-time signaling or client hub layer)
 ┣ 📜 [YourProjectName].slnx / .sln     (Integrated workspace layout)
 ┗ 📂 .git/                             (Version control initialization)
```

---

## 🚀 Getting Started

### 1. Requirements
Ensure the following tools are globally available in your machine's environment variables (`PATH`):
*   **[.NET SDK](https://microsoft.com)** (Supports .NET 9.0 up to .NET 11.0+)
*   **[Git](https://git-scm.com)**

### 2. Compilation and Local Run
Clone your repository, navigate to the source directory, and run the program:

```bash
dotnet run
```
You will be prompted to supply a workspace name. Enter your project name (e.g., `ShopSphere`) to generate the complete backend workspace.

---

## 🏗️ Cross-Platform Deployment

`dn` can be published as a self-contained, standalone single executable that executes on devices **without requiring a pre-installed .NET Runtime**.

### One-Line Global Release Build
Chain these native compilation commands in your terminal to build distributions for Windows, Linux, and macOS simultaneously:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true && dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true && dotnet publish -c Release -r osx-arm64 --self-contained true -p:PublishSingleFile=true
```

### Build Distributions
Your production binaries will be generated under the following build paths:
*   **Windows x64:** `.\bin\Release\net11.0\win-x64\publish\dn.exe`
*   **Linux x64:** `.\bin\Release\net11.0\linux-x64\publish\dn`
*   **Apple Silicon Mac:** `.\bin\Release\net11.0\osx-arm64\publish\dn`

---

## ⚙️ Post-Release Configuration

### Linux & macOS Executable Permission Fix
Operating systems running Unix-based kernels strip execution tags out of downloaded binary bundles. Mark your native output file executable before running:
```bash
chmod +x ./dn
```

### macOS Gatekeeper Workaround
If macOS blocks the script execution citing an unverified developer warning, clear Apple's structural security quarantine tracker:
```bash
xattr -d com.apple.quarantine ./dn
```

---

## 🌍 Global Access: Run `dn` from Anywhere

To launch your workspace factory by typing `dn` in any folder or terminal window on your machine, wire it into your system's global paths:

### 🪟 Windows Setup (PowerShell)
1. Move your compiled `dn.exe` to a dedicated tools folder (e.g., `C:\tools\dn.exe`).
2. Run this command as **Administrator** to inject it permanently into your environment profile:
   ```powershell
   [Environment]::SetEnvironmentVariable("Path", \$env:Path + ";C:\tools", "User")
   ```
3. Restart your terminal application.

### 🍎 macOS & 🐧 Linux Setup
1. Copy the compiled binary out to your local environment system bin:
   ```bash
   sudo cp ./dn /usr/local/bin/dn
   sudo chmod +x /usr/local/bin/dn
   ```
2. Open a new terminal instance.

---

## 🪵 Diagnostic Handling & `.slnx` Operations

### Modern Solution Formats
In modern configurations, the .NET CLI creates an XML-based solution structure (`.slnx`) instead of legacy `.sln` layout configuration maps. `dn` handles this tracking dynamically by actively indexing the directory patterns at runtime to bind libraries accurately regardless of the system SDK version.

### Global Interceptions
All unexpected filesystem failures, directory naming structural blockages, or underlying shell execution errors are captured natively by `GlobalExceptionHandler`. Critical application logs are cleanly piped directly into `error_log.txt` generated locally in the working terminal profile.
