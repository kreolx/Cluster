# Предметная область
- есть кластер (сеть) из нескольких узлов (серверов)
- каждый узел может находиться в одном из двух состояний - работоспособен или недоступен
- каждый узел может быть связан с произвольным кол-вом других узлов
- каждый узел может передать сигнал другому работоспособному узлу, если он связан с ним напрямую или опосредованно через другие работоспособные узлы
- кластер считается "расщеплённым" (splitted), если есть хотя бы два работоспособных узла, которые не могут передать друг другу сигнал

# Описание компонентов
- IClusterClient - интерфейс описывающий взаимодействие с кластером, предоставляет методы:
  - получить список всех узлов кластера
  - получить состояние узла кластера
  - получить список узлов связанных с указанным узлом кластера
- ClusterClientMock - реализация мока для IClusterClient для проверки работоспособности решения, дополнительно предоставляет методы:
  - отключить указанный узел
  - сделать весь кластер недоступным
- ClusterMonitor - класс осуществляющий мониторинг кластера, публикует события:
  - состояние кластера изменилось - кластер стал расщеплённым или наоборот перестал быть расщеплённым

# Задача
- реализовать метод Program.IsSplittedAsync:
  - метод должен возвращать true, если кластер расщеплён, иначе возвращать false
- доработать класс ClusterMonitor:
  - событие ClusterStateChanged должно публиковаться, если кластер стал расщеплённым или наоборот перестал быть расщеплённым (указывается параметром IsSplitted)

# Условия
- нельзя менять интерфейс IClusterClient
- нельзя менять поведение ClusterClientMock
