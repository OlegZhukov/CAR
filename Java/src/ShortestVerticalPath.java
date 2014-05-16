import java.util.Arrays;

class ShortestVerticalPath {
    private int n;
    private int[] e;

    public final int[] distTo;
    public final int[] prev;

    public ShortestVerticalPath(int[] e, int n, int[] distTo, int[] prev) {
        this.n = n;
        this.e = e;
        this.distTo = distTo;
        this.prev = prev;
    }

    public int doFind() {
        initDistTo();
        int nm = e.length;
        relaxLeftmost(0);
        int rightmost = n - 1;
        for (int i = 0;; i++) {
            if (i == rightmost) {
                relaxRightmost(i++);
                rightmost += n;
                if (rightmost == nm - 1) break;
                relaxLeftmost(i);
            }
            else relax(i);
        }
        int result = nm - n;
        for (int i = result + 1; i < nm; i++)
            if (distTo[i] < distTo[result]) result = i;
        return result;
    }

    private void initDistTo() {
        Arrays.fill(distTo, 0, n - 1, 0);
        Arrays.fill(distTo, n, distTo.length, Integer.MAX_VALUE);
    }

    private void relaxLeftmost(int i) {
        relaxBorder(i, 1);
    }

    private void relaxRightmost(int i) {
        relaxBorder(i, -1);
    }

    private void relaxBorder(int i, int shift) {
        int bottom = i + n, nextToBottom = bottom + shift;
        int newDistToBottom = distTo[i] + e[bottom], newDistToNextToBottom =
                distTo[i] + e[nextToBottom];
        if (newDistToBottom < distTo[bottom]) {
            distTo[bottom] = newDistToBottom;
            prev[bottom] = i;
        }
        if (newDistToNextToBottom < distTo[nextToBottom]) {
            distTo[nextToBottom] = newDistToNextToBottom;
            prev[nextToBottom] = i;
        }
    }

    private void relax(int i) {
        int bottom = i + n, rightBottom = bottom + 1, leftBottom = bottom - 1;
        int newDistToBottom = distTo[i] + e[bottom], newDistToRightBottom =
                distTo[i] + e[rightBottom], newDistToLeftBottom =
                distTo[i] + e[leftBottom];
        if (newDistToBottom < distTo[bottom]) {
            distTo[bottom] = newDistToBottom;
            prev[bottom] = i;
        }
        if (newDistToRightBottom < distTo[rightBottom]) {
            distTo[rightBottom] = newDistToRightBottom;
            prev[rightBottom] = i;
        }
        if (newDistToLeftBottom < distTo[leftBottom]) {
            distTo[leftBottom] = newDistToLeftBottom;
            prev[leftBottom] = i;
        }
    }
}
