import numpy as np
import matplotlib.pyplot as plt

M1, M2, M3 = np.random.uniform(0, 10, 3)
myu1, myu2, myu3 = np.random.uniform(0, 0.5, 3)

n = 6
g = 10
time_points = np.arange(0, n, 1)  # n= 6
F = np.random.uniform(-300, 300)
x1 = y1 = x2 = y2 = x3 = y3 = 0


def calculateAccelleration(vel, myu1, myu2, myu3):
    if (vel[2] > 0):
        myu3 *= -1
    if (vel[0] < 0):
        myu1 *= -1
    if (vel[1] < 0):
        myu2 *= -1

    matrix = np.array([
        [myu1 + 1, M1, 0, 0],
        [1, 0, -M2, 0],
        [1, 0, 0, -M3],
        [0, 1, -1, -1]
    ])

    b = np.array([
        F - myu1 * g * (M1 + M2),
        myu2 * M2 * g,
        M3 * g - myu3 * F,
        0
    ])

    acc = (np.linalg.inv(matrix) @ b)[-1:0:-1]

    new = [None, None, None]
    if vel[1] == 0 and acc[0] ** 2 < (g * myu1) ** 2:
        new[0] = 0
        matrix = np.delete(matrix, 0, 0)
        matrix = np.delete(matrix, 0, 1)
        b = np.delete(b, 1)
    if vel[1] == 0 and acc[1] ** 2 < (g * myu2) ** 2:
        new[1] = 0
        if len(matrix) == 4:
            matrix = np.delete(matrix, 1, 0)
            matrix = np.delete(matrix, 1, 1)
            b = np.delete(b, 1)
        elif len(matrix) == 3:
            matrix = np.delete(matrix, 0, 0)
            matrix = np.delete(matrix, 0, 1)
            b = np.delete(b, 0)
    if vel[2] == 0 and (acc[2] * M3) ** 2 < (F * myu3) ** 2:
        new[2] = 0
        if len(matrix) == 4:
            matrix = np.delete(matrix, 2, 0)
            matrix = np.delete(matrix, 2, 1)
            b = np.delete(b, 2)
        elif len(matrix) == 3:
            matrix = np.delete(matrix, 1, 0)
            matrix = np.delete(matrix, 1, 1)
            b = np.delete(b, 1)
        elif len(matrix) == 2:
            matrix = np.delete(matrix, 0, 0)
            matrix = np.delete(matrix, 0, 1)
            b = np.delete(b, 0)

    acc = (np.linalg.inv(matrix) @ b)[-1:0:-1]
    ind = 0
    for i in range(3):
        if new[i] == None:
            new[i] = acc[ind]
            ind += 1

    return np.array(new)


velocity = np.array([[0, 0, 0]])
kordinatner = np.array([[x1, x2, y3]])
for i in range(n - 1):
    acceleration = calculateAccelleration(velocity[i], myu1, myu2, myu3)
    velocity = np.append(velocity, [velocity[i] + acceleration], 0)
    kordinatner = np.append(kordinatner, [
        acceleration * time_points[i] * time_points[i] / 2 + velocity[i] * time_points[i] + kordinatner[i]], 0)

x1, y1 = (kordinatner[:, 0], np.ones(n) * y1)
x2, y2 = (kordinatner[:, 1], np.ones(n) * y2)
x3, y3 = (x1, kordinatner[:, 2])

plt.xlabel("t")
plt.ylabel("coordinate")

plt.plot(time_points, x1)
plt.plot(time_points, x2)
plt.plot(time_points, y3)
plt.show()
