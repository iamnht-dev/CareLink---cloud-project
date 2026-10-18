window.careLink = (() => {
    let context, timer;
    const beep = () => {
        if (!context || context.state !== 'running') return;
        const oscillator = context.createOscillator(), gain = context.createGain();
        oscillator.connect(gain); gain.connect(context.destination);
        oscillator.frequency.value = 660;
        gain.gain.setValueAtTime(0.12, context.currentTime);
        gain.gain.exponentialRampToValueAtTime(0.001, context.currentTime + 0.5);
        oscillator.start(); oscillator.stop(context.currentTime + 0.5);
    };
    const stop = () => { clearInterval(timer); timer = undefined; };
    window.addEventListener('pagehide', stop);
    return {
        load: () => localStorage.getItem('carelink.v1'),
        save: value => localStorage.setItem('carelink.v1', value),
        enable: async () => {
            context ??= new AudioContext();
            await context.resume();
            beep();
            return context.state === 'running';
        },
        alarm: active => { if (!active) stop(); else if (!timer) { beep(); timer = setInterval(beep, 1800); } },
        stop
    };
})();
