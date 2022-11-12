import matplotlib.pyplot as plt

def bounds(N, ax):
    x  = [ 0, N, 0, 0]
    y = [ 0, 0, N, 0]
    ax.plot(x, y, color='red');
    mid = N * 0.5
    ax.text(mid, mid, '$I = 0$', rotation=-45)
    ax.set_aspect(1.0)
    ax.axis('off')
    ax.annotate('$R = 0$ ($S$ axis)', xy=(-0.05, 0.25), xycoords='axes fraction',
             xytext=(20, 20), textcoords='offset points',
             ha="center", va="bottom", rotation=90)
    ax.annotate('$S = 0$ ($R$ axis)', xy=(0.5, 0.07), xycoords='axes fraction',
             xytext=(-20, -20), textcoords='offset points',
             ha="center", va="bottom")

def draw(N, s, r):
    fig, ax = plt.subplots(1)
    bounds(N, ax)
    ax.plot(r, s, color='black');
    plt.show()

def sir(N, t, s, i, r, t_label):
    # Plot the data on three separate curves for S(t), I(t) and R(t)
    fig = plt.figure(facecolor='w')
    ax = fig.add_subplot(111, facecolor='#dddddd', axisbelow=True)
    ax.plot(t, s, 'b', alpha=0.5, lw=2, label='Susceptible')
    ax.plot(t, i, 'r', alpha=0.5, lw=2, label='Infectious')
    ax.plot(t, r, 'g', alpha=0.5, lw=2, label='Resolved')
    ax.set_xlabel(t_label)
    ax.set_ylabel('Number')
    ax.set_xlim(0, t[-1])
    ax.set_ylim(0, N)
    legend = ax.legend()
    legend.get_frame().set_alpha(0.5)
    ax.set_facecolor((1.0, 1.0, 1.0))
    for spine in ('top', 'right'):
        ax.spines[spine].set_visible(False)
    plt.show()


