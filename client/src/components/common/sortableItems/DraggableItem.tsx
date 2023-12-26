import { DragOverlay } from "@dnd-kit/core";

interface Props {
    activeItem: React.ReactNode
}

export function DraggableItem({ activeItem }: Props) {
    return <DragOverlay>
        {activeItem}
    </DragOverlay>
}