# Senior Game Engineer Assignment - SuperHuge Studios

Welcome to the Senior Game Engineer assignment!

In this exercise, you’ll build a simple two-player `Tug-Of-War` game in Unity.

---

## Overview

- Two scenes are provided:

  1. **LOBBY Scene** – Where you look at summary, and jump into next game.
  2. **GAMEPLAY Scene** – The actual Tug-Of-War mechanics between two players.

- Your tasks are broken into 3 parts:

  1. **GAMEPLAY logic** (Reference Image Provided)
  2. **Loading Screen**
  3. **Progress & State Restore Logic**

- There are 2 bonus challenges as well which are optional.

- Use **Unity Version**: 2022.3.xx LTS.
- Ideally this entire exercise should take around 6-8 hours.

---

## Submission Guidelines

1. Clone this repository.
2. Complete assignment.
3. Upload and share your repository link (preferred). OR
   - send a ZIP of the project (without libraries.)

---

## 📝 Task 1: Implement Tug-Of-War GAMEPLAY

Recreate the Tug-Of-War mechanic.

1. **GAMEPLAY Screen Layout**

   - The screen is split into two equal halves:
     - **Top Half**: Opponent’s tap area
     - **Bottom Half**: Your tap area
   - A rope is hanging in the center. You can use anything = Image / Graphic / 3D object, etc.
   - Two horizontal dashed line, indicating your area, and opponent area. No hands / text, etc needed.

1. **Objective**

- Track taps rate for each player.
- The rope should be moving towards the player who taps more quickly.
- The game ends when:
  - You WIN = The rope marker touches the dashed line in bottom half (=your area).
  - You LOSE = The rope marker touches the dashed line in top half (=opponent area).
  - Timeout = The game has been running for 30 seconds with no result.
- The game should be running locally. No multiplayer needed. You can use `two fingers` OR `keyboard vs mouse` to simulate 2 players.

Reference Image:
![ref image](ReferenceImage.png)

---

## 📝 Task 2: Loading Screen

When switching between **LOBBY ↔ Gameplay**, show a loading screen that:

- Show a slider indicating progress percentage.
- Display a text telling **X %**

NOTE:

- The loading screen must remain visible for **at least 2 seconds**, even if the target scene finishes loading faster.

**[BONUS Challenge] Prevent “Frozen” Feel**

- Come up with an idea so that the app doesn't look stuck while in the loading screen.

---

## 📝 Task 3: Progress & State

1.  Add 4 texts (simple TextMeshPro labels) that show always in both LOBBY and GAMEPLAY scene on top-right corner:
    - Text 1: Time spent in this scene = `{x} hr {y} min {z} sec`
    - Text 2: Time spent in this session = `{x} hr {y} min {z} sec`
    - Text 3: Time spent (today) = `{x} hr {y} min {z} sec`
    - Text 4: Time spent (lifetime) = `{x} hr {y} min {z} sec`
2.  If needed show as `{w} days {x} hr {y} min {z} sec` as well.
3.  These should be updating every second.
4.  These 4 texts should NOT be showing in the loading screen.

---

## 📝 Task 4: **[BONUS Challenge]** – Dynamic Asset Download & Instantiation

- Download a prefab from internet, and dynamically instantiate it on the screen.
- You may use: Github Pages / firebase / s3 to host your file.
- It is intentionally open ended. You may download just a simple 3d cube and instantiate.

---

## NOTE:

- This task is designed to assess your understanding of core Unity concepts. The primary focus should be on functionality and code quality.
- You are encouraged to use any publicly available tutorials or resources(image/code) for guidance, but the final code should be your own.
- For any clarification - please reply to the email thread, or mail to `rahul@xarpie.com`.

---

This is your opportunity to demonstrate your skills.
Take your time, focus on the essentials, and good luck!
