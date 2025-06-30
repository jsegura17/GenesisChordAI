# MusicGuideApp

MusicGuideApp es una aplicación musical diseñada para ayudar a músicos a tocar canciones en vivo, proporcionando herramientas interactivas para la práctica y la interpretación.

## Propósito
El objetivo principal de la aplicación es permitir cargar una canción y asistir al músico durante la ejecución en vivo, mostrando acordes y letras sincronizadas, y permitiendo el control individual de las pistas de audio.

## Características principales
- **Carga y gestión de canciones**: Permite seleccionar y cargar canciones desde una biblioteca.
- **Visualización en tiempo real**:
  - Muestra el acorde actual y la línea de la letra correspondiente, sincronizados con la reproducción.
  - Barra de progreso para seguir el avance de la canción.
- **Control de pistas de audio**:
  - Ajuste de volumen y muteo individual de cada pista (por ejemplo, bajo, batería, guitarra, etc.).
  - Reproducción, pausa, avance y retroceso de la canción.
- **Información musical relevante**:
  - Título, artista, tempo y tonalidad de la canción.

## Estructura del proyecto
- **Client/Models**: Modelos de datos como `Song`, `AudioTrack`, `Section` y `LyricLine`.
- **Client/Components**: Componentes de interfaz para la reproducción de audio, visualización de acordes, letras y gestión de la biblioteca de canciones.
- **Client/Services**: Servicios para la gestión de audio y almacenamiento.

## Modelo de Canción (`Song`)
```csharp
public class Song
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public int Tempo { get; set; }
    public string Key { get; set; } = string.Empty;
    public List<AudioTrack> AudioTracks { get; set; } = new();
    public List<Section> Sections { get; set; } = new();
}
```

## Ejemplo de uso
1. Selecciona una canción desde la biblioteca.
2. Visualiza los acordes y letras sincronizados mientras se reproduce la canción.
3. Ajusta el volumen o silencia pistas individuales según tus necesidades para practicar o tocar en vivo.

## Público objetivo
- Músicos que desean practicar o tocar en vivo con acompañamiento.
- Bandas que necesitan una guía visual y auditiva durante sus presentaciones.

---

Proyecto inicial. En desarrollo.
