// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

Start();

static void Start()
{
    int[,] board = new int[9, 9] {
        {5, 3, 0, 0, 7, 0, 0, 0, 0},
        {6, 0, 0, 1, 9, 5, 0, 0, 0},
        {0, 9, 8, 0, 0, 0, 0, 6, 0},

        {8, 0, 0, 0, 6, 0, 0, 0, 3},
        {4, 0, 0, 8, 0, 3, 0, 0, 1},
        {7, 0, 0, 0, 2, 0, 0, 0, 6},

        {0, 6, 0, 0, 0, 0, 2, 8, 0},
        {0, 0, 0, 4, 1, 9, 0, 0, 5},
        {0, 0, 0, 0, 8, 0, 0, 7, 9}
    };

    Console.WriteLine("Sudoku before solving:");
    PrintSudoku(board);

    if (SolveSudoku(board))
    {
        Console.WriteLine("Sudoku solved:");
        PrintSudoku(board);
    }
    else
    {
        Console.WriteLine("Could not solve the Sudoku");
    }

    Console.ReadKey();
}


static void PrintSudoku(int[,] board)
{
    for (int i = 0; i < 9; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            Console.Write($"{board[i, j]} ");
            if ((j + 1) % 3 == 0 && j != 8)
            {
                Console.Write("| ");
            }
        }
        Console.WriteLine();
        if ((i + 1) % 3 == 0 && i != 8)
        {
            Console.WriteLine("---------------------");
        }
    }
}

static bool IsValid(int[,] board, int row, int col, int num)
{
    // بررسی ستون
    for (int i = 0; i < 9; i++)
    {
        if (board[row, i] == num)
        {
            return false;
        }
    }

    // بررسی ردیف
    for (int i = 0; i < 9; i++)
    {
        if (board[i, col] == num)
        {
            return false;
        }
    }

    // بررسی بلوک 3x3
    int blockRow = row / 3 * 3;
    int blockCol = col / 3 * 3;

    for (int i = blockRow; i < blockRow + 3; i++)
    {
        for (int j = blockCol; j < blockCol + 3; j++)
        {
            if (board[i, j] == num)
            {
                return false;
            }
        }
    }

    // اگر هیچکدام از شرایط بالا برقرار نبود، مقدار درست است.
    return true;
}

static bool SolveSudoku(int[,] board)
{
    // گام اول: پیدا کردن یک خانه خالی در صفحه سودوکو
    int row = -1;
    int col = -1;
    bool isEmpty = true;

    for (int i = 0; i < 9; i++)
    {
        for (int j = 0; j < 9; j++)
        {
            if (board[i, j] == 0)
            {
                row = i;
                col = j;

                // مشخص کردن اینکه آیا هنوز خانه خالی دیگری وجود دارد یا نه
                isEmpty = false;
                break;
            }
        }
        if (!isEmpty)
        {
            break;
        }
    }

    // اگر هیچ خانه خالی‌ای پیدا نشد، صفحه سودوکو حل شده است.
    if (isEmpty)
    {
        return true;
    }

    // گام دوم: پر کردن خانه خالی با اعداد از 1 تا 9 و بررسی صحت آن‌ها
    for (int num = 1; num <= 9; num++)
    {
        if (IsValid(board, row, col, num))
        {
            board[row, col] = num;

            // گام سوم: با استفاده از بازگشت، حل سودوکو را به صورت بازگشتی ادامه می‌دهیم
            if (SolveSudoku(board))
            {
                return true;
            }

            // اگر حل سودوکو با اعداد فعلی امکان پذیر نبود، خانه را دوباره خالی می‌کنیم و با اعداد دیگری تلاش می‌کنیم.
            board[row, col] = 0;
        }
    }

    // اگر هیچ عددی برای پر کردن خانه خالی پیدا نشد، برنامه به خطا خورده است و صفحه سودوکو حل نشده است.
    return false;
}

