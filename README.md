Weak Passwords & Brute-Force Lab
A complete, self-contained classroom lab on how weak passwords fall to
brute-force attacks. Everything runs on each student's own laptop on
`localhost`, so it works even when students and teachers are on different
VLANs — no traffic leaves the machine, and there is no PHP or web host to set up.
One Visual Studio solution with two projects, plus the lesson paperwork.
What's in here
```
BruteForceLab.sln
├── TargetWebsite/        ASP.NET site the student RUNS (the thing being attacked)
│   ├── Program.cs        the deliberately-insecure /validate endpoint
│   └── wwwroot/          login page, success/failure pages, CSS
├── BruteForceClient/     console app the student MODIFIES (the attacker)
│   └── Program.cs        menu-driven starter (see below)
└── docs/
    ├── Student Worksheet - Weak Passwords and Brute force attacks.docx
    │                      hand this to students
    └── Teacher Notes & Answer Guide.docx
                           model answers + marking rubric (teacher only)
```
`TargetWebsite` is insecure on purpose: `/validate` accepts credentials over
GET or POST, compares them in plaintext, and has no rate limiting or lockout.
That is the lesson — please don't "fix" it.
The two accounts
Demo account — give this to students: username `demo`, password `letmein`.
Students use it to see what a successful login looks like (and a failed one,
by entering a wrong password). These credentials are printed in the worksheet.
Target account — the one students crack: username `bob`, password is a
whole number between 1 and 99 (ships as `42`).
Students: open and run (Visual Studio 2026)
Get the code. In Visual Studio choose Clone a repository and paste the
link your teacher gives you. (Or use Code → Download ZIP on GitHub and open
`BruteForceLab.sln`.)
Start the target site. Right-click TargetWebsite → Set as Startup
Project, then press Ctrl+F5 (Start without debugging, so it keeps
running). A browser opens to `http://localhost:5000`. Leave it running.
Run the attacker. Right-click BruteForceClient → Set as Startup
Project, then press F5. A menu appears with three options:
Show the web page — the program acts like a web browser and just
fetches a page (a quick "is it connected?" check).
Do a login — you type a username and password and it sends that one
attempt. Use the demo account to see success, then a wrong password to see
failure.
Run my own code — the empty `StudentTask()` method where you write the
solution.
Do the task. Follow the student worksheet in `docs/`.
Tip: to launch both projects at once, use Solution → Properties → Startup
Project → Multiple startup projects and set both to Start.
Teacher setup
Set bob's password in `TargetWebsite/Program.cs` to the number you want the
class to crack (ships as `42`). The demo account (`demo` / `letmein`) can be
left as is.
Fill in the bracketed placeholders in the student worksheet (your repo link,
and how students submit).
Test the clone → open → run path on one student laptop image before the
lesson, especially the .NET version (see note below).
Keep `docs/Teacher Notes & Answer Guide.docx` for yourself — it has model
answers and a marking rubric.
.NET version note
The projects target .NET 8 (LTS). Visual Studio 2026 ships with a newer
.NET, so if it offers to install .NET 8, accept — or just open each `.csproj`
and change `<TargetFramework>net8.0</TargetFramework>` to the version your VS
already has (e.g. `net9.0` / `net10.0`). The code is identical either way.
A note on the starter code
The menu starter gives students two building blocks (fetching a page, and
sending a single login they type in) but the `StudentTask()` method is empty.
Writing the loop that tries each password and detects success is the student's
task — that is the whole point of the exercise, so the working attack code is
not included here.
Ethics and the law
Brute-forcing a login is only lawful on a system you own or have clear
permission to test — like this practice site running on a student's own laptop.
Doing it to any other site, account or system without permission is a serious
criminal offence in Australia (Criminal Code Act 1995 (Cth), Part 10.7; Crimes
Act 1958 (Vic), s 247G). The worksheet covers this for students. Verify exact
section numbers and penalties on austlii.edu.au before distributing.
