# Tank One Button Only

**English** | [Русский](#русский)

A small 3D Unity arcade game created as a Game Jam project. Control a tank using just one button, aim carefully, and destroy every target before running out of ammunition.

## Gameplay

The tank rotates automatically when the button is not held. Press **Space** to fire, hold it to drive forward, and release it to reverse the direction of rotation. Every shot counts!

- Destroy all targets to win.
- Some targets move, making them harder to hit.
- Running out of ammunition while targets remain ends the round.
- The game restarts automatically after a win or loss.

## Controls

| Input | Action |
| --- | --- |
| Press **Space** | Fire a projectile |
| Hold **Space** | Move forward |
| Release **Space** | Reverse rotation direction |
| No input | Rotate in place |

## Built with

- Unity (3D)
- C#
- Unity Input System
- Rigidbody physics

## Implemented systems

- One-button tank movement, rotation, and shooting
- Projectile collisions, hit effects, and camera shake
- Random target spawning and moving targets
- Ammunition and remaining-target counters
- Win/lose conditions and automatic scene restart
- Background music and shooting sound support

## Source code

Key scripts: `TankOneButtonController.cs`, `Projectile.cs`, `MovingTarget.cs`, `Target.cs`, `GameManager.cs`, `CameraFollow.cs`, and `MusicPlayer.cs`.

---

## Русский

Небольшая 3D аркада на Unity, созданная в рамках Game Jam. Управляй танком всего одной кнопкой, выбирай момент для выстрела и уничтожь все цели до того, как закончатся снаряды.

## Геймплей

Когда кнопка не нажата, танк вращается на месте. Нажатие **пробела** вызывает выстрел, удержание заставляет танк двигаться вперёд, а отпускание меняет направление вращения.

- Для победы нужно уничтожить все цели.
- Часть целей движется.
- Если боеприпасы закончились, а цели остались, раунд проигран.
- После победы или поражения игра автоматически перезапускается.

## Управление

| Действие | Результат |
| --- | --- |
| Нажать **пробел** | Выстрел |
| Удерживать **пробел** | Движение вперёд |
| Отпустить **пробел** | Смена направления вращения |
| Не нажимать | Вращение на месте |

## Технологии

- Unity (3D)
- C#
- Unity Input System
- Физика Rigidbody

## Реализованные механики

- Управление танком одной кнопкой
- Стрельба, столкновения снарядов и эффекты попаданий
- Случайное появление целей и движущиеся мишени
- Счётчики боеприпасов и оставшихся целей
- Победа, поражение и автоматический перезапуск
- Фоновая музыка и звуки выстрелов

## Исходный код

Основные скрипты: `TankOneButtonController.cs`, `Projectile.cs`, `MovingTarget.cs`, `Target.cs`, `GameManager.cs`, `CameraFollow.cs` и `MusicPlayer.cs`.
