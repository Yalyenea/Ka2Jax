# Ka2Jax
---

### **Ka2Jax Application Overview**

**Description:**
Ka2Jax is a Windows desktop application designed to convert mathematical formulas from KaTeX format to MathJax format. It provides a simple and intuitive interface for users to input KaTeX formulas and get the corresponding MathJax output.

### **Key Features:**

1. KaTeX to MathJax Conversion:
   - Converts `\(` and `\)` to `$`
   - Converts `\[` and `\]` to `$$`
   - Handles whitespace around delimiters

2. Clipboard Monitoring:
   - Automatically detects and converts KaTeX formulas copied to the clipboard
   - Updates clipboard with converted MathJax format

3. User Interface:
   - Input text box for KaTeX formulas
   - Output text box displaying converted MathJax
   - Copy button to copy converted output to clipboard

### **How to Build:**

1. Install .NET 6.0 SDK

2. Clone the repository:

   ```bash
   git clone https://github.com/yalyenea/Ka2Jax.git
   ```

3. Navigate to project directory:

   ```bash
   cd Ka2Jax
   ```

4. Build and run the project:

   ```bash
   dotnet build && dotnet run --project .
   ```

5. For release build:

   ```bash
   dotnet build -c Release
   ```

### **How to Run:**

1. Ensure .NET 6.0 Runtime is installed on your system
2. Navigate to the application directory:
   ```bash
   cd ./Ka2Jax/bin/Debug/net6.0-windows/
   ```
3. Run the executable:
   ```bash
   Ka2Jax.exe
   ```

### **System Requirements:**

- Operating System: Windows 7 SP1 / 8.1 / 10 / 11 (64-bit)
- .NET Runtime: .NET 6.0 Desktop Runtime
- RAM: Minimum 1GB
- Disk Space: Minimum 200MB
