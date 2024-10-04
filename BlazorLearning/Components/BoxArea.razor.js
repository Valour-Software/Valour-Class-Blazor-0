export const getPosition = (element) => {
    const bounds = element.getBoundingClientRect();
    return {
        clientX: bounds.left,
        clientY: bounds.top
    };
};
//# sourceMappingURL=BoxArea.razor.js.map