import os, os.path, nbformat
from nbconvert.preprocessors import ExecutePreprocessor

template = ""
with open("template.ipynb") as f:
    template = f.read()

ep = ExecutePreprocessor(timeout=600, kernel_name='python3')

for path in os.listdir('.'):
    bits = os.path.splitext(path)
    if bits[1] != '.csv':
        continue
    index = bits[0]
    print(path)
    nb = template.replace("%CSV%", path)
    notebook = nbformat.reads(nb, as_version=nbformat.NO_CONVERT)
    ep.preprocess(notebook, {'metadata': {'path': '.'}})
    output = "%s.ipynb" % index
    nbformat.write(notebook, open(output, mode='wt'))
