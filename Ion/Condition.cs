namespace Ion;

public delegate bool Condition();

public delegate bool Condition<X>(X x);

public delegate bool Condition<X, Y>(X x, Y y);

public delegate bool Condition<X, Y, Z>(X x, Y y, Z z);