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