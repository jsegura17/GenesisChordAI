import numpy as np
from scipy.io import wavfile

def generate_test_tone(filename, duration=5, freq=440):
    # Frecuencia de muestreo
    sample_rate = 44100
    t = np.linspace(0, duration, int(sample_rate * duration))
    # Generar tono
    signal = np.sin(2 * np.pi * freq * t)
    # Normalizar y convertir a int16
    signal = np.int16(signal * 32767)
    # Guardar archivo
    wavfile.write(filename, sample_rate, signal)

# Generar tonos de prueba
generate_test_tone('test-audio.wav', duration=30, freq=440)  # Tono A
generate_test_tone('chord-guide.wav', duration=30, freq=523.25)  # Tono C