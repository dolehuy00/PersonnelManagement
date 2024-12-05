import React from "react";
import {
    Pagination,
    PaginationItem,
    PaginationLink
} from "reactstrap";

const CustomPagination = ({ pageIndex, totalPage, maxPageView, onPageChange }) => {
    // Hàm chuyển đến trang trước
    const handlePrevPage = () => {
        if (pageIndex > 1) {
            onPageChange(pageIndex - 1);
        }
    };

    // Hàm chuyển đến trang tiếp theo
    const handleNextPage = () => {
        if (pageIndex < totalPage) {
            onPageChange(pageIndex + 1);
        }
    };

    // Hàm xử lý khi người dùng chọn một trang cụ thể
    const handlePageClick = (page) => {
        onPageChange(page);
    };

    // Xác định trang bắt đầu và kết thúc trong phạm vi hiển thị
    const getPageRange = () => {
        const halfMax = Math.floor(maxPageView / 2);
        let start = Math.max(1, pageIndex - halfMax);
        let end = Math.min(totalPage, pageIndex + halfMax);

        if (end - start + 1 < maxPageView) {
            if (start === 1) {
                end = Math.min(totalPage, start + maxPageView - 1);
            } else {
                start = Math.max(1, end - maxPageView + 1);
            }
        }
        return { start, end };
    };

    const { start, end } = getPageRange();

    return (
        <nav aria-label="...">
            <Pagination
                className="pagination justify-content-end mb-0"
                listClassName="justify-content-end mb-0"
            >
                <PaginationItem className={pageIndex === 1 ? "disabled" : ""}>
                    <PaginationLink
                        href="#prev-to-head"
                        onClick={(e) => {
                            e.preventDefault();
                            if (pageIndex !== 1) handlePageClick(1);
                        }}
                        tabIndex="-1"
                    >
                        <i className="fas fa-angle-left" />
                        <i className="fas fa-angle-left" />
                        <span className="sr-only">Previous To Head</span>
                    </PaginationLink>
                </PaginationItem>
                <PaginationItem className={pageIndex === 1 ? "disabled" : ""}>
                    <PaginationLink
                        href="#prev"
                        onClick={(e) => {
                            e.preventDefault();
                            if (pageIndex !== 1) handlePrevPage();
                        }}
                    >
                        <i className="fas fa-angle-left" />
                        <span className="sr-only">Previous</span>
                    </PaginationLink>
                </PaginationItem>
                {/* Dấu chấm "..." khi trang hiện tại cách xa trang 1 */}
                {start > 1 && (
                    <PaginationItem className="disabled">
                        <PaginationLink
                            href="#threedot-prev"
                            onClick={(e) => e.preventDefault()}
                        >
                            <i className="fas fa-ellipsis-h"></i>
                            <span className="sr-only">Three Dot Previous</span>
                        </PaginationLink>
                    </PaginationItem>
                )}

                {/* Hiển thị các trang trong phạm vi start-end */}
                {[...Array(end - start + 1)].map((_, i) => {
                    const pageNum = start + i;
                    return (
                        <PaginationItem className={pageIndex === pageNum ? "active" : ""} key={"select-page-" + i}>
                            <PaginationLink
                                href={`#page-${pageNum}`}
                                onClick={(e) => { e.preventDefault(); onPageChange(pageNum); }}
                            >
                                {pageNum}
                            </PaginationLink>
                        </PaginationItem>
                    );
                })}

                {/* Dấu chấm "..." khi trang hiện tại cách xa trang cuối */}
                {end < totalPage && (
                    <PaginationItem className="disabled">
                        <PaginationLink
                            href="#threedot-next"
                            onClick={(e) => e.preventDefault()}
                        >
                            <i className="fas fa-ellipsis-h"></i>
                            <span className="sr-only">Three Dot Next</span>
                        </PaginationLink>
                    </PaginationItem>
                )}
                <PaginationItem className={pageIndex === totalPage ? "disabled" : ""}>
                    <PaginationLink
                        href="#next"
                        onClick={(e) => {
                            e.preventDefault();
                            if (pageIndex !== totalPage) handleNextPage();
                        }}
                    >
                        <i className="fas fa-angle-right" />
                        <span className="sr-only">Next</span>
                    </PaginationLink>
                </PaginationItem>
                <PaginationItem className={pageIndex === totalPage ? "disabled" : ""}>
                    <PaginationLink
                        href="#next-to-last"
                        onClick={(e) => {
                            e.preventDefault();
                            if (pageIndex !== totalPage) handlePageClick(totalPage);
                        }}
                    >
                        <i className="fas fa-angle-right" />
                        <i className="fas fa-angle-right" />
                        <span className="sr-only">Next To Last</span>
                    </PaginationLink>
                </PaginationItem>
            </Pagination>
        </nav>
    );
};

export default CustomPagination;
