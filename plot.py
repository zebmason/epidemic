import sys
import os.path
import triangle

def load(file_path):
    t = []
    s = []
    i = []
    r = []
    ssd = []
    isd = []
    rsd = []
    for line in open(file_path):
        if line[0:1] == '#':
            continue

        bits = line.split(',')
        if len(bits) == 6:
            t.append(1.0 * len(t))
            s.append(float(bits[0]))
            i.append(float(bits[2]))
            r.append(float(bits[4]))
            ssd.append(float(bits[1]))
            isd.append(float(bits[3]))
            rsd.append(float(bits[5]))
        elif len(bits) == 3:
            t.append(1.0 * len(t))
            s.append(float(bits[0]))
            i.append(float(bits[1]))
            r.append(float(bits[2]))
    return t, s, i, r, ssd, isd, rsd

def confidence_interval(m, sd, factor):
    l = []
    u = []
    for i in range(len(m)):
        l.append(m[i] - factor * sd[i])
        u.append(m[i] + factor * sd[i])
    return l, u
