# Derivation of deterministic epidemic models from a discrete stochastic process

The enclosed code and data accompany a paper submitted to various journals.

## Simulating epidemics

Launch Microsoft Visual Studio, build and run the two executables to create
`.csv` files containing the simulation results in the directories `SIStudy`
and `SIRStudy` corresponding to SI and SIR models respectively.

## Data analysis (SI)

Individual Jupyter notebooks pre-exist for each `.csv` file.

## Data analysis (SIR)

In the `SIRStudy` directory run the Python script `notebooks.py` to create
individual Jupyter notebooks for each `.csv` file. The pre-existing Jupyter
notebooks there split off the correlations for each of the three denominators
* aS + bI + cR
* aS + b(I + R)
* aS + bR

## Data analysis (Tag)

A cut down version of the SIR study for the game of epidemiological tag on
[substack](https://https://zebm.substack.com/p/epidemiological-tag/).
