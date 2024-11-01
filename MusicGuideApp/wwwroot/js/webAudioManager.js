// wwwroot/js/webAudioManager.js

let audioContext;
let tracks = new Map();
let dotNetRef;
let currentTime = 0;
let isPlaying = false;
let startTime = 0;
let pauseTime = 0;

export function initialize(dotNetReference) {
    try {
        dotNetRef = dotNetReference;
        audioContext = new (window.AudioContext || window.webkitAudioContext)();
        console.log("Audio context initialized successfully");

        // Start the update loop
        requestAnimationFrame(updateTime);
        return true;
    } catch (error) {
        console.error("Error initializing audio context:", error);
        return false;
    }
}

export async function loadTrack(trackId, url) {
    try {
        const response = await fetch(url);
        const arrayBuffer = await response.arrayBuffer();
        const audioBuffer = await audioContext.decodeAudioData(arrayBuffer);

        const track = {
            buffer: audioBuffer,
            source: null,
            gainNode: audioContext.createGain(),
            startTime: 0,
            pauseTime: 0
        };

        track.gainNode.connect(audioContext.destination);
        tracks.set(trackId, track);
        console.log(`Track ${trackId} loaded successfully`);
        return true;
    } catch (error) {
        console.error(`Error loading track ${trackId}:`, error);
        return false;
    }
}

export function play(trackId) {
    try {
        const track = tracks.get(trackId);
        if (!track) {
            console.warn(`Track ${trackId} not found`);
            return false;
        }

        if (track.source) {
            track.source.stop();
        }

        track.source = audioContext.createBufferSource();
        track.source.buffer = track.buffer;
        track.source.connect(track.gainNode);

        const offset = track.pauseTime;
        track.startTime = audioContext.currentTime - offset;
        track.source.start(0, offset);

        isPlaying = true;
        updatePlaybackState();
        console.log(`Playing track ${trackId}`);
        return true;
    } catch (error) {
        console.error(`Error playing track ${trackId}:`, error);
        return false;
    }
}

export function pause() {
    try {
        for (const [trackId, track] of tracks.entries()) {
            if (track.source) {
                track.pauseTime = audioContext.currentTime - track.startTime;
                track.source.stop();
                track.source = null;
            }
        }

        isPlaying = false;
        updatePlaybackState();
        console.log("Playback paused");
        return true;
    } catch (error) {
        console.error("Error pausing playback:", error);
        return false;
    }
}

export function stop() {
    try {
        for (const [trackId, track] of tracks.entries()) {
            if (track.source) {
                track.source.stop();
                track.source = null;
            }
            track.pauseTime = 0;
            track.startTime = 0;
        }

        isPlaying = false;
        currentTime = 0;
        updatePlaybackState();
        console.log("Playback stopped");
        return true;
    } catch (error) {
        console.error("Error stopping playback:", error);
        return false;
    }
}

export function setVolume(trackId, volume) {
    try {
        const track = tracks.get(trackId);
        if (track) {
            track.gainNode.gain.value = Math.max(0, Math.min(1, volume));
            console.log(`Volume set to ${volume} for track ${trackId}`);
            return true;
        }
        console.warn(`Track ${trackId} not found`);
        return false;
    } catch (error) {
        console.error(`Error setting volume for track ${trackId}:`, error);
        return false;
    }
}

export function setTime(time) {
    try {
        if (time < 0) time = 0;

        for (const [trackId, track] of tracks.entries()) {
            if (track.source) {
                track.source.stop();
                track.source = null;
            }
            track.pauseTime = time;
        }

        if (isPlaying) {
            // Si estaba reproduciendo, reiniciar la reproducción desde la nueva posición
            for (const [trackId, track] of tracks.entries()) {
                play(trackId);
            }
        }

        currentTime = time;
        console.log(`Playback time set to ${time}`);
        return true;
    } catch (error) {
        console.error("Error setting playback time:", error);
        return false;
    }
}

export function getCurrentTime() {
    if (!isPlaying) return currentTime;

    const track = Array.from(tracks.values())[0]; // Get first track for time reference
    if (track) {
        return audioContext.currentTime - track.startTime;
    }
    return 0;
}

export function getPlaybackState() {
    return isPlaying ? 'Playing' : 'Paused';
}

function updateTime() {
    if (isPlaying) {
        const track = Array.from(tracks.values())[0]; // Get first track for time reference
        if (track) {
            currentTime = audioContext.currentTime - track.startTime;
            dotNetRef.invokeMethodAsync('UpdateTime', currentTime);
        }
    }
    requestAnimationFrame(updateTime);
}

function updatePlaybackState() {
    const state = isPlaying ? 'Playing' : 'Paused';
    dotNetRef.invokeMethodAsync('UpdatePlaybackState', state);
}