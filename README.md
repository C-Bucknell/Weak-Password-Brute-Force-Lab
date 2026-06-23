# Weak Passwords & Brute-Force Lab

A complete, self-contained classroom lab on how weak passwords fall to
brute-force attacks. Everything runs on each student's own laptop on
`localhost`, so it works even when students and teachers are on different
VLANs — no traffic leaves the machine, and there is no PHP or web host to set up.

One Visual Studio solution with two projects, plus the lesson paperwork.

## What's in here

```
BruteForceLab.sln
├── TargetWebsite/        ASP.NET site the student RUNS (the thing being attacked)
│   ├── Program.cs        the deliberately-insecure /validate endpoint
│   └── wwwroot/          login page, success/failure pages, CSS
├── BruteForceClient/     console app the student MODIFIES (the attacker)
│   └── Program.cs        the starter
└── docs/
    ├── Student-Worksheet-Weak-Passwords-and-Brute-force-attacks.docx     hand this to students
    └── Teacher-Notes-&-Answer-Guide.docx    model answers + teacher notes (teacher only)
```

`TargetWebsite` is insecure on purpose: `/validate` accepts credentials over
GET or POST, compares them in plaintext, and has no rate limiting or lockout.
That is the lesson — please don't "fix" it.

## Students: open and run (Visual Studio 2026)

1. **Get the code.** In Visual Studio choose **Clone a repository** and paste the
   link your teacher gives you. (Or use **Code → Download ZIP** on GitHub and open
   `BruteForceLab.sln`.)
2. **Start the target site.** Right-click **TargetWebsite → Set as Startup
   Project**, then press **Ctrl+F5** (Start *without* debugging, so it keeps
   running). A browser opens to `http://localhost:5000`. Leave it running.
3. **Run the attacker.** Right-click **BruteForceClient → Set as Startup
   Project**, then press **F5**. The console connects to the site and prints the
   page it got back — that confirms everything is wired up.
4. **Do the task.** Follow `docs/Student-Worksheet.docx`.

Tip: to launch both at once, use **Solution → Properties → Startup Project →
Multiple startup projects** and set both to **Start**.

## Teacher setup

1. Set bob's password in `TargetWebsite/Program.cs` to the number you want the
   class to crack (ships as `42`).
2. Fill in the bracketed placeholders in `docs/Student-Worksheet.docx` (your
   repo link, and how students submit).
3. Test the clone → open → run path on one student laptop image before the
   lesson, especially the .NET version (see note below).
4. Keep `docs/Teacher-Answer-Key.docx` for yourself — it has model answers and a
   marking rubric.

## .NET version note

The projects target **.NET 8 (LTS)**. Visual Studio 2026 ships with a newer
.NET, so if it offers to install .NET 8, accept — or just open each `.csproj`
and change `<TargetFramework>net8.0</TargetFramework>` to the version your VS
already has (e.g. `net9.0` / `net10.0`). The code is identical either way.

## A note on the starter code

`BruteForceClient/Program.cs` deliberately stops at "send one request and read
the response." Writing the loop that tries each password and detects success is
the student's task — that is the whole point of the exercise, so the working
attack code is not included here.

## Ethics

Brute-forcing a login is only acceptable on a system you own or have clear
permission to test — like this practice site running on a student's own laptop.
Never against any other site or account.
