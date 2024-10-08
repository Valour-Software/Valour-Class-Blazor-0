export const getPosition = (element) => {
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
//# sourceMappingURL=BoxArea.razor.js.map