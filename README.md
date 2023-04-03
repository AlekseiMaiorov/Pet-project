# Keep Balance Ball 3D

![](https://github.com/AlekseiMaiorov/Keep-Balance-Ball-3D/blob/6d5ae8ecd60ca81f897c1f583d6262c6152c8572/Images/Keep_Balance_Ball_3D.gif)

**Описание:**
Игроку предстоит прыгать по платформам и удерживать равновесие.

**Цель игры:**
Набрать как можно больше очков.

## Реализация:
[Короткое видео](https://youtu.be/hNgIh3W-AME)

### Архитектура

![](https://github.com/AlekseiMaiorov/Keep-Balance-Ball-3D/blob/528748e9facab1ecf980d3c312a2daf000f2693c/Images/1.png "Жизненный цикл игры")

* Жизненным циклом игры управляет класс `GameStateMachine`.
* Сервисы создаются и внедряются с помощью Zenject.
* В игре есть сохранение и загрузка очков в PlayerPrefs.

### Загрузка ассетов
Ассеты загружаются с помощью Addressables.

### Игрок

<img height="300" src="https://github.com/AlekseiMaiorov/Keep-Balance-Ball-3D/blob/6d5ae8ecd60ca81f897c1f583d6262c6152c8572/Images/3.PNG" title="Компоненты игрока"/>

* Функционал игрока разделен по компонентам.
* Компоненты инициализируются через методы `Construct` в `PlayerFactory`.
* Передвижение игрока релизовано с использованием `Rigidbody`.
* Пользовательский ввод работает через Input System (клавиатура и тачскрин).
* Настройки персонажа загружаются из конфига игрока.

<img height="300" src="https://github.com/AlekseiMaiorov/Keep-Balance-Ball-3D/blob/6d5ae8ecd60ca81f897c1f583d6262c6152c8572/Images/5.PNG" title="Конфиг игрока"/>

### Платформы

![](https://github.com/AlekseiMaiorov/Keep-Balance-Ball-3D/blob/6d5ae8ecd60ca81f897c1f583d6262c6152c8572/Images/4.gif)

* За создание и инициализацию платформ отвечает `PlatformFactory`.
* Платформы контролируются скриптом `PlatformMover`. 
* Платформы перемещаются с начала в конец по достижению игроком триггера на платформе.
* Платформы не создаются каждый раз заново, а содержатся в пуле.
* Для управления перемещением платформ используется очередь `Queue<Platform>`.
* Настройки платформ загружаются из конфига игрока.

<img height="300" src="https://github.com/AlekseiMaiorov/Keep-Balance-Ball-3D/blob/6d5ae8ecd60ca81f897c1f583d6262c6152c8572/Images/2.PNG" title="Конфиг платформ"/>

### UI

* За создание и инициализацию UI отвечает `UIFactory`.
* Интерфейс создан с возможностью запуска на разных экранах (адаптивный).


### Плагины, использованные в проекте:
* DOTween
* Zenject
* UniTask
* Addressables
* InputSystem