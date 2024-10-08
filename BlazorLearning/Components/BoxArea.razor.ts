type ElementPosition = {
    clientX: number;
    clientY: number;
};

export const getPosition = (element: HTMLElement): ElementPosition => {
    const bounds = element.getBoundingClientRect();
    return {
        clientX: bounds.left,
        clientY: bounds.top
    };
};

export const playBounceSound = async () => {
    const audio = new Audio('bounce.mp3');
    await audio.play();
};